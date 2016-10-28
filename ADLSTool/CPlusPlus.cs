using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Azure.Management.DataLake.Store.Models;

namespace Microsoft.Azure.Management.DataLake.StoreUploader
{
    class CPlusPlus
    {
        private static string newPath;
        public static void CWrapper(List<FileStatusProperties> fileList, string adlsAccountName, string adlsUserName, string adlsPin, string path, string DestPath, string clientId, string subId, string UpDowndLoad)
        {
            
            List<ProcessStartInfo> infos = new List<ProcessStartInfo>();
            string Currentpath = System.IO.Directory.GetCurrentDirectory();
            newPath = Path.GetFullPath(Path.Combine(Currentpath, @"..\adltool"));
            Console.WriteLine(newPath);
            foreach (var item in fileList)
            {
                //string cmd = @"downloadfilefe  -l e:\engress\" + item.PathSuffix + " -w CaboCabo234$ -m adlsperf@microsoft.com -ls login.windows.net -e https://perfanalysis.azuredatalakestore.net/webhdfs/v1/ -p ADLSPERFD14C/v-qitie/100-1GB/" + item.PathSuffix;
                string cmd = @UpDowndLoad+" -l "+ DestPath + item.PathSuffix +" -w "+ adlsPin+" -m "+ adlsUserName+ " -ls login.windows.net " + "-e " + "https://"+ adlsAccountName+ ".azuredatalakestore.net/webhdfs/v1/"+" -p "+ path+ item.PathSuffix;
                infos.Add(new ProcessStartInfo(@newPath+@"\adltool.exe", cmd));               
            }

            StartProcesses(infos);
        }

        //for upload file
        public static void CWrapper(string adlsAccountName, string adlsUserName, string adlsPin, string path, string DestPath, string clientId, string subId, string UpDowndLoad)
        {
            //ProcessStartInfo[] infos = null;
            List<ProcessStartInfo> infos = new List<ProcessStartInfo>();
            string Currentpath = System.IO.Directory.GetCurrentDirectory();
            newPath = Path.GetFullPath(Path.Combine(Currentpath, @"..\adltool"));
            //string[] fileEntries = Directory.GetFiles(DestPath);
            DirectoryInfo d = new DirectoryInfo(@DestPath);//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("*.*");
            foreach (FileInfo file in Files)
            {
                //string cmd = @"uploadfilefe -l e:\engress\" + item.PathSuffix + @" -w CaboCabo234$ -m adlsperf@microsoft.com -ls login.windows.net -e https://perfanalysis.azuredatalakestore.net/webhdfs/v1/ -p ADLSPERFD14C/v-qitie/100-1GB/" + item.PathSuffix;
                string cmd = @UpDowndLoad + " -l " + DestPath + file.Name + " -w " + adlsPin + " -m " + adlsUserName + " -ls login.windows.net " + "-e " + "https://" + adlsAccountName + ".azuredatalakestore.net/webhdfs/v1/" + " -p " + path + file.Name;
                infos.Add(new ProcessStartInfo(@newPath + @"\adltool.exe", cmd));

            }

           StartProcesses(infos);
        }

        // Process all files in the directory passed in, recurse on any directories 
        // that are found, and process the files they contain.
        //public static void ProcessDirectory(string targetDirectory)
        //{
        //    // Process the list of files found in the directory.
        //    string[] fileEntries = Directory.GetFiles(targetDirectory);
        //    foreach (string fileName in fileEntries)
        //       // ProcessFile(fileName);

        //    // Recurse into subdirectories of this directory.
        //    string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
        //    foreach (string subdirectory in subdirectoryEntries)
        //        ProcessDirectory(subdirectory);
        //}

        public static void StartProcesses(List<ProcessStartInfo> infos)
        {
            //int count = 0;
            foreach(var info in infos)
            {
                // Make sure the Hidden flag is set.
                //info.WindowStyle = ProcessWindowStyle.Hidden;
                info.WindowStyle = ProcessWindowStyle.Hidden;
                info.UseShellExecute = false;
                info.WorkingDirectory = @newPath;
                
                Process process = Process.Start(info);
                Thread.Sleep(2000);
                Console.WriteLine("started");
                //count++;
                //if (count == 50)
                //{
                //    Thread.Sleep(1000 * 30);
                //    count = 0;
                //}
            }

        }
    }
}
