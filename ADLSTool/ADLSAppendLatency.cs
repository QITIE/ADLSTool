using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Microsoft.Azure.Management.DataLake.Store;
using Microsoft.Azure.Management.DataLake.StoreUploader;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Rest;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;

namespace Rentrak_MLD_Load
{
    class Program
    {
        #region constants and private members
        private static DataLakeStoreFileSystemManagementClient _dataLakeStoreFileSystemClient;
        private static CloudBlobClient _cloudBlobClient;
        private static string _adlsUserName = "adlsperf@microsoft.com";
        private static string _adlsPin = "CaboCabo345$";
        // SET THESE VALUES FOR YOUR RUN FOR APPEND LATENCY FOR ADLS
        private const string dataLakeAccountName = "adlsperf12compencdm3";
        private const string applicationClientId = "4bbed59f-ca62-43ad-9bcd-fdb39490f25f";
        private const string destFolder = "/Powershell/CADLSPERF1/100GB";
        private const string folder = "";
        // END APPEND LATENCY TEST CONSTANTS FOR ADLS

        // BEGIN APPEND LATENCY TEST CONSTANTS FOR WASB
        private const string wasbAccount = "cwasbtest";
        private const string wasbKey = "Y0kW7X6MSoo6tdWWwyEHTqerZGK11SZHKgcvwa7xpZa0ZQdxj8Z6IF0VygKsUCKOP048vMlso2HbUmlAGhqQGw==";
        private const string wasbContainer = "appendlatency";
        private const string wasbBlob = "https://cwasbtest.blob.core.windows.net/";
        // END APPEND LATENCY TEST CONSTANT FOR WASB

        // MODIFY THESE IF YOU HAVE DIFFERENT FILES
        private const string localLargeFileName = @"E:\data\MicrosoftTelemetry.tsv"; // 2.5GB perf test binary file
        private const string localFileName = @"E:\data\smallFile.txt"; // 4mb perf test binary file
        private const string local10gbFileName = @"E:\data\FixedBlockPerfData.txt"; // 10GB perf test binary file
        private const string localFolderUploadName = @"D:\Egress"; // 100000 1mb files in 4 nested folders.
        // END FILE CONSTANTS FOR UPLOAD DOWNLOAD TESTS

        #endregion
        static void Main(string[] args)
        {
            #region Set up ADLS access.
            // SET THIS
            var subscriptionId = new Guid("46153750-fa3b-4140-bf57-8beb7d5c971a");
            var applicationClientId = "1950a258-227b-4e31-a9cf-717495945fc2";
            var redirectUri = new Uri("urn:ietf:wg:oauth:2.0:oob");
            var _credentials = AuthenticateUser("common", "https://management.core.windows.net/", applicationClientId, redirectUri);
            var appendArray = new byte[32 * 1024]; //32kb
            SetupClients(_credentials, subscriptionId.ToString());
            GenerateFileData(appendArray);
            #endregion

            
            /*
            if (!File.Exists(localFileName))
            {
                FileStream fs = new FileStream(localFileName, FileMode.OpenOrCreate);
                fs.Seek((4 * 1024 * 1024)-1, SeekOrigin.Begin);
                fs.WriteByte(0);
                fs.Close();
            }

            if (!File.Exists((localLargeFileName)))
            {
                FileStream fs = new FileStream(localLargeFileName, FileMode.OpenOrCreate);
                fs.Seek((long)(2.5 * 1024 * 1024 * 1024) - 1, SeekOrigin.Begin);
                fs.WriteByte(0);
                fs.Close();
            }

            if (!File.Exists((local10gbFileName)))
            {
                FileStream fs = new FileStream(local10gbFileName, FileMode.OpenOrCreate);
                fs.Seek(10737418240 - 1, SeekOrigin.Begin); //10gb
                fs.WriteByte(0);
                fs.Close();
            }
            */

            // change expect100 to false
            ServicePointManager.Expect100Continue = false;

            //Console.WriteLine("File and Folder Upload Measurements");

            // MeasureUploadFilePerformance(4, "mb", 10, localFileName);

            //MeasureUploadFilePerformance(2.5, "gb", 1, localLargeFileName);

            //MeasureUploadFilePerformance(10, "gb", 1, local10gbFileName);

            //MeasureUploadFolderPerformance(4, "mb", 5000, false, 3, localFolderUploadName);

            //Console.WriteLine("File and Folder Download Measurements");

            //MeasureDownloadFilePerformance(32, "kb", 10, localFileName + ".out");

            //MeasureDownloadFilePerformance(4, "mb", 10, localFileName + ".out");

            //MeasureDownloadFilePerformance(2.5, "gb", 5, localLargeFileName + ".out");

            // MeasureDownloadFilePerformance(10, "gb", 1, local10gbFileName + ".out");

            MeasureDownloadFolderPerformance(1, "GB", 50, false, 1, localFolderUploadName);

            //MeasureUploadFolderPerformance(1, "mb", 100000, false, 1, localFolderUploadName);

            //MeasureAppendPerformance(32, "kb", 100, appendArray, true);
            //MeasureAppendPerformance(32, "kb", 100, appendArray, false);

            Console.ReadKey();
        }

        public static byte[] GetBytesFromFile()
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                _dataLakeStoreFileSystemClient.FileSystem.OpenAsync(dataLakeAccountName, "begoldsm/testsmall.txt").Result.CopyTo(ms);
                byte[] bytes = ms.GetBuffer();
                ms.Close();
                ms.Dispose();
                return bytes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void MeasureAppendPerformance(double dataSize, string dataSizeModifier, int iterations, byte[] appendArray, bool isAdlsAppend)
        {
            Console.WriteLine(string.Format("Appending {0}{1} of data {2} times to {3}...", dataSize, dataSizeModifier, iterations, isAdlsAppend ? ", ADLS" : "WASB"));
            List<long> perfMetrics;
            if (isAdlsAppend)
            {
                string destLocation = string.Format("{0}/{1}/{2}{3}File.txt", destFolder, folder, dataSize, dataSizeModifier);
                perfMetrics = AppendToAdlsFile(_dataLakeStoreFileSystemClient, dataLakeAccountName, destLocation, iterations, appendArray);
            }
            else
            {
                perfMetrics = AppendToWasbContainer(_cloudBlobClient, wasbContainer, wasbBlob, iterations, appendArray);
            }

            var averageLatency = perfMetrics.Average();
            
            Console.WriteLine(string.Format("Average latency in milliseconds for {0} runs is: {1}", iterations, averageLatency));
            Debug.WriteLine(string.Format("Average latency in milliseconds for {0} runs is: {1}", iterations, averageLatency));

        }

        public static void MeasureUploadFolderPerformance(double dataSize, string dataSizeModifer, int numFiles, bool recursive, int iterations, string folderToUpload)
        {
            bool force = true;  //Set this to true if you want to overwrite existing data
            var perfMetrics = new List<long>();
            Console.WriteLine(string.Format("Uploading {0} files of {1}{2} data{3}...", numFiles, dataSize, dataSizeModifer, recursive ? ", recursively" : string.Empty));
            for (int i = 0; i < iterations; ++i)
            {
                string destLocation = string.Format("{0}/{1}/folderUpload", destFolder, folder);
                var watch = Stopwatch.StartNew();
                UploadFile(_dataLakeStoreFileSystemClient, dataLakeAccountName, folderToUpload, destLocation, force, recursive);
                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
                Console.WriteLine("Folder Uploaded : " + i);
                perfMetrics.Add(elapsedMs);
            }

            foreach (long perf in perfMetrics)
            {
                Console.WriteLine(perf);
                Debug.WriteLine(perf);
            }
        }

        public static void MeasureDownloadFolderPerformance(double dataSize, string dataSizeModifer, int numFiles, bool recursive, int iterations, string destinationFolder)
        {
            bool force = true;  //Set this to true if you want to overwrite existing data
            var perfMetrics = new List<long>();
            Console.WriteLine(string.Format("Downloading {0} files of {1}{2} data{3}...", numFiles, dataSize, dataSizeModifer, recursive ? ", recursively" : string.Empty));
            for (int i = 0; i < iterations; ++i)
            {
                string sourceLocation = string.Format("{0}/{1}", destFolder, folder);
                var watch = Stopwatch.StartNew();
                DownloadFile(_dataLakeStoreFileSystemClient, dataLakeAccountName, sourceLocation, destinationFolder, force);
                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
                Console.WriteLine("Folder Downloaded : " + i);
                perfMetrics.Add(elapsedMs);
            }

            foreach (long perf in perfMetrics)
            {
                Console.WriteLine(perf);
                Debug.WriteLine(perf);
            }
        }

        public static void MeasureUploadFilePerformance(double dataSize, string dataSizeModifier, int iterations, string fileToUpload)
        {
            List<long> perfMetrics = new List<long>();

            // Upload data
            bool force = true;  //Set this to true if you want to overwrite existing data

            Console.WriteLine(string.Format("Uploading {0}{1} data...", dataSize, dataSizeModifier));
            for (int i = 0; i < iterations; ++i)
            {
                string destLocation = string.Format("{0}/{1}/{2}{3}File.txt", destFolder, folder, dataSize, dataSizeModifier);
                var watch = Stopwatch.StartNew();
                UploadFile(_dataLakeStoreFileSystemClient, dataLakeAccountName, fileToUpload, destLocation, force);
                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
                Console.WriteLine("File Uploaded : " + i);
                perfMetrics.Add(elapsedMs);
            }

            foreach (long perf in perfMetrics)
            {
                Console.WriteLine(perf);
                Debug.WriteLine(perf);
            }
        }

        public static void MeasureDownloadFilePerformance(double dataSize, string dataSizeModifier, int iterations, string destination)
        {
            List<long> perfMetrics = new List<long>();

            // download data
            bool force = true;  //Set this to true if you want to overwrite existing data

            Console.WriteLine(string.Format("Downloading {0}{1} data...", dataSize, dataSizeModifier));
            for (int i = 0; i < iterations; ++i)
            {
                string sourceLocation = string.Format("{0}/{1}/{2}{3}File.txt", destFolder, folder, dataSize, dataSizeModifier);
                var watch = Stopwatch.StartNew();
                DownloadFile(_dataLakeStoreFileSystemClient, dataLakeAccountName, sourceLocation, destination, force);
                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
                Console.WriteLine("File downloaded : " + i);
                perfMetrics.Add(elapsedMs);
            }

            foreach (long perf in perfMetrics)
            {
                Console.WriteLine(perf);
                Debug.WriteLine(perf);
            }
        }

        // Authenticate the user with AAD through an interactive popup.
        // You need to have an application registered with AAD in order to authenticate.
        //   For more information and instructions on how to register your application with AAD, see: 
        //   https://azure.microsoft.com/en-us/documentation/articles/resource-group-create-service-principal-portal/
        public static TokenCredentials AuthenticateUser(string tenantId, string resource, string appClientId, Uri appRedirectUri, string userId = "")
        {
            //var authContext = new AuthenticationContext("https://login.microsoftonline.com/" + tenantId);

            //var tokenAuthResult = authContext.AcquireToken(resource, appClientId, appRedirectUri,
            //  PromptBehavior.Always, UserIdentifier.AnyUser);

            //return new TokenCredentials(tokenAuthResult.AccessToken);
            return new TokenCredentials(GetAccessToken(@"https://login.windows.net/common", resource, appClientId).GetAwaiter().GetResult());
        }

        private static async Task<string> GetAccessToken(string authority, string resource, string clientId)
        {
            //var clientCredential = new ClientCredential(
            //    "e679c3a3 - 40eb - 413e-b428 - 78ddfcb9f695",
            //    "pUAiP0KdSnqkD6cyhdHimnq");

            var context = new AuthenticationContext(
                authority,
                TokenCache.DefaultShared);

            //var result = await context.AcquireTokenAsync(resource, "1950a258-227b-4e31-a9cf-717495945fc2", new Uri("urn:ietf:wg:oauth:2.0:oob"), new PlatformParameters(PromptBehavior.Auto, null), UserIdentifier.AnyUser);
            AuthenticationResult result = null;
            int i = 10;
            while (i > 0)
            {
                try
                {
                    result = await context.AcquireTokenAsync(resource, clientId, new UserPasswordCredential(_adlsUserName, _adlsPin));
                    return result.AccessToken;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    i--;
                }
            }
            return result.AccessToken;
        }

        //Set up clients
        public static void SetupClients(TokenCredentials tokenCreds, string subscriptionId)
        {
            _dataLakeStoreFileSystemClient = new DataLakeStoreFileSystemManagementClient(tokenCreds);
            var storageCreds = new StorageCredentials(wasbAccount, wasbKey);
            _cloudBlobClient = new CloudBlobClient(new Uri(string.Format("https://{0}.blob.core.windows.net/", wasbAccount)), storageCreds);
        }


        public static bool CreateDir(DataLakeStoreFileSystemManagementClient dataLakeStoreFileSystemClient, string path, string dlAccountName, string permission)
        {
            dataLakeStoreFileSystemClient.FileSystem.Mkdirs(path, dlAccountName);
            return true;
        }

        public static bool UploadFile(DataLakeStoreFileSystemManagementClient dataLakeStoreFileSystemClient, string dlAccountName, string srcPath, string destPath, bool force = false, bool recursive = false, bool testCancel = false)
        {
            var cancelSource = new CancellationTokenSource();
            var myToken = cancelSource.Token;
            var parameters = new UploadParameters(srcPath, destPath, dlAccountName, isOverwrite: force, isBinary: true, perFileThreadCount: 40, concurrentFileCount: 100, isRecursive: recursive);
            var progressTracker = new System.Progress<UploadFolderProgress>();
            progressTracker.ProgressChanged += (s, e) =>
            {
                if (e.TotalFileCount == 0)
                {
                    Console.WriteLine("we are done!");
                }
            };
            var frontend = new DataLakeStoreFrontEndAdapter(dlAccountName, dataLakeStoreFileSystemClient, myToken);
            var uploader = new DataLakeStoreUploader(parameters, frontend, myToken, folderProgressTracker: progressTracker);
            if (testCancel)
            {
                var uploadTask = Task.Run(() =>
                {
                    myToken.ThrowIfCancellationRequested();
                    uploader.Execute();
                    myToken.ThrowIfCancellationRequested();
                }, myToken);

                try
                {
                    while (!uploadTask.IsCompleted && !uploadTask.IsCanceled)
                    {
                        if (myToken.IsCancellationRequested)
                        {
                            // we are done tracking progress and will just break and let the task clean itself up.
                            try
                            {
                                uploadTask.Wait();
                            }
                            catch (OperationCanceledException)
                            {
                                if (uploadTask.IsCanceled)
                                {
                                    uploadTask.Dispose();
                                }
                            }
                            catch (AggregateException ex)
                            {
                                if (ex.InnerExceptions.OfType<OperationCanceledException>().Any())
                                {
                                    if (uploadTask.IsCanceled)
                                    {
                                        uploadTask.Dispose();
                                    }
                                }
                                else
                                {
                                    throw;
                                }
                            }
                            catch (Exception ex)
                            {
                                // swallow this for debugging to see what it is.
                            }

                            break;
                        }

                        Thread.Sleep(60000);
                        // run for 60 seconds and then cancel out and see what happens
                        cancelSource.Cancel();
                    }
                }
                catch (OperationCanceledException)
                {
                    // do nothing since we successfully cancelled out
                }
                catch (Exception ex)
                {
                    // see what the heck is going on.
                }
            }
            else
            {
                uploader.Execute();
            }
            return true;
        }

        public static bool DownloadFile(DataLakeStoreFileSystemManagementClient dataLakeStoreFileSystemClient, string dlAccountName, string srcPath, string destPath, bool force = false, bool recursive = false)
        {
            var parameters = new UploadParameters(srcPath, destPath, dlAccountName, isOverwrite: force, isBinary: true, isDownload: true, perFileThreadCount: 40, concurrentFileCount: 100, isRecursive: recursive);
            var frontend = new DataLakeStoreFrontEndAdapter(dlAccountName, dataLakeStoreFileSystemClient);
            var uploader = new DataLakeStoreUploader(parameters, frontend);
            uploader.Execute();
            return true;
        }

        public static List<long> AppendToAdlsFile(DataLakeStoreFileSystemManagementClient datalakeStoreFileSystemClient, string dlAccountName, string destFile, int numIterations, byte[] contents)
        {
            // always overwrite the file at the beginning to ensure a fresh empty file
            datalakeStoreFileSystemClient.FileSystem.Create(dlAccountName, destFile, new MemoryStream(), overwrite: true);
            var perfMetrics = new List<long>();
            for (int i = 0; i < numIterations; i++)
            {
                
                using (var stream = new MemoryStream(contents))
                {
                    var watch = Stopwatch.StartNew();
                    datalakeStoreFileSystemClient.FileSystem.Append(dlAccountName, destFile, stream);
                    watch.Stop();
                    perfMetrics.Add(watch.ElapsedMilliseconds);
                }
                
            }

            return perfMetrics;
        }

        public static List<long> AppendToWasbContainer(CloudBlobClient blobClient, string containerName, string blobName, int numIterations, byte[] contents)
        {
            CloudBlobContainer appendContainer = blobClient.GetContainerReference(containerName);
            CloudAppendBlob appendBlob = appendContainer.GetAppendBlobReference(blobName);
            appendBlob.CreateOrReplace();
            appendContainer.CreateIfNotExists();
            var perfMetrics = new List<long>();
            for (int i = 0; i < numIterations; i++)
            {
                using (var stream = new MemoryStream(contents))
                {
                    var watch = Stopwatch.StartNew();
                    appendBlob.AppendBlock(stream);
                    watch.Stop();
                    perfMetrics.Add(watch.ElapsedMilliseconds);
                }
            }

            return perfMetrics;
        }

        /// <summary>
        /// Generates some random data and writes it out to an in-memory array
        /// </summary>
        /// <param name="contents">The array to write random data to.</param>
        internal static void GenerateFileData(byte[] contents)
        {
            var rnd = new Random(0);
            rnd.NextBytes(contents);
        }

    }
}

