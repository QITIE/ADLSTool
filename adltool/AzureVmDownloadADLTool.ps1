# The following commands should be run from a powershell admin prompt to invoke this script

# winrm s winrm/config/client '@{TrustedHosts="*"}'
# Invoke-Command -computername workernode0,workernode1,workernode2,workernode3,workernode4,workernode5,workernode6,workernode7 -filepath <localPathOnVM>\AzureVmDownloadADLTool.ps1 -ArgumentList passwd,configMafsPath,blobPath,runAsUser,runAsPwd

# Download config action module from a well-known directory.

param([string]$passwd = "****", [string]$configPath = "****", [string]$blobFilePath = "****", [string]$runAsUser = "****", [string]$runAsPwd = "****")

$CONFIGACTIONURI = "https://hdiconfigactions.blob.core.windows.net/configactionmodulev02/HDInsightUtilities-v02.psm1"
$CONFIGACTIONMODULE = "C:\HDInsightUtilities.psm1"
$webclient = New-Object System.Net.WebClient
$webclient.DownloadFile($CONFIGACTIONURI, $CONFIGACTIONMODULE)
# (TIP) Import config action helper method module to make writing config action easy.
if (Test-Path ($CONFIGACTIONMODULE))
{ 
    Import-Module $CONFIGACTIONMODULE
} 
else
{
    Write-Output "Failed to load HDInsightUtilities module, exiting ..."
    exit
}

# Remove the adltool scheduled task if any, delete the adltool zip file and extracted dir
Stop-Process -processname adltool
schtasks /delete /TN adltool /F
Remove-Item c:\adlperf -Force -Recurse
Remove-Item C:\adlperf.zip

# Download the adltool zip file
# $blobFilePath = "https://useaststorageadlperf.blob.core.windows.net/perfbin/adlperf.zip"
$localFilePath = "C:\adlperf.zip"
Save-HDIFile -SrcUri $blobFilePath -DestFile $localFilePath
$hostName = hostname
Write-HDILog "Done with copying ADLTool on $hostName - $localFilePath at: $(Get-Date)"

# Now extract the adltool zip file
$localExtractDir = "C:\"
$extractCmd = "jar -xvf $localFilePath"
Set-Location $localExtractDir
Invoke-Expression $extractCmd
Write-HDILog "Extracted ADLTool"

# Schedule a task to run 'adltool runmafsfilecmdfe <params>' every 4 hours
schtasks.exe /Create /SC HOURLY /MO 4 /TN adltool /TR "C:\adlperf\adltool\AzureVmStart.bat $passwd $configPath" /RU $runAsUser /RP $runAsPwd
