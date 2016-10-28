using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.IO;
using System.Security;
using Microsoft.Azure.Management.DataLake.Store;
using Microsoft.Azure.Management.DataLake.Store.Models;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Azure.KeyVault;
using Microsoft.Rest;

namespace Microsoft.Azure.Management.DataLake.StoreUploader
{
    class Program
    {
        private static DataLakeStoreAccountManagementClient _adlsClient;
        private static DataLakeStoreFileSystemManagementClient _adlsFileSystemClient;

        private static string _adlsAccountName;
        private static string _adlsUserName;
        private static string _adlsPin;
        private static string _Path;
        private static string _DestPath;
        private static string _clientId;
        private static string _subId;
        private static string _location;
        private static string _UpDowndLoad;
        private static string localLargeFileName = @"E:\ingress\2-1024-1\1024-1-1.dat";
        static void Main(string[] args)
        {
            //_adlsAccountName = "perfanalysis"; // TODO: Replace this value with the name for a NEW Store account.
            //_resourceGroupName = "ADLS-PERF"; // TODO: Replace this value. This resource group should already exist.
            _location = "East US 2";
            try
            {
                _UpDowndLoad = args[0];
                _adlsUserName = args[1];
                _adlsPin = args[2];
                _adlsAccountName = args[3];
                _Path = args[4];
                _DestPath = args[5];
                _clientId = args[6];
                _subId = args[7];
                 
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid parameters");
            }
            
            // Authenticate the user client id:1950a258-227b-4e31-a9cf-717495945fc2
            var tokenCreds = AuthenticateUser("common", "https://management.core.windows.net/",
                _clientId, new Uri("urn:ietf:wg:oauth:2.0:oob"), _clientId); // TODO: Replace bracketed values.

            //setup client subid:9e1f0ab2-2f85-49de-9677-9da6f829b914
            SetupClients(tokenCreds, _subId); // TODO: Replace bracketed value.

            //if (_UpDowndLoad == "downloadfilefe")
            //{
            //    var itemList = ListItems(_Path); 
            //     CPlusPlus.CWrapper(itemList, _adlsAccountName, _adlsUserName, _adlsPin, _Path, _DestPath, _clientId, _subId, _UpDowndLoad);
            //}
            //else if(_UpDowndLoad == "uploadfilefe")
            //{
            //     CPlusPlus.CWrapper(_adlsAccountName, _adlsUserName, _adlsPin, _Path, _DestPath, _clientId, _subId, _UpDowndLoad);
            //}
            //Console.WriteLine("Finished at {0}", DateTime.Now.TimeOfDay);
            //Console.ReadKey();
            
            MeasureUploadFilePerformance(2.5, "gb", 1, localLargeFileName);
    }

        public static void MeasureUploadFilePerformance(double dataSize, string dataSizeModifier, int iterations, string fileToUpload)
        {
            List<long> perfMetrics = new List<long>();

            // Upload data
            bool force = true;  //Set this to true if you want to overwrite existing data

            Console.WriteLine(string.Format("Uploading {0}{1} data...", dataSize, dataSizeModifier));
            for (int i = 0; i < iterations; ++i)
            {
                string destLocation = string.Format("{0}1024-1-1.dat", _Path);
                var watch = Stopwatch.StartNew();
                UploadFile(_adlsFileSystemClient, _adlsAccountName, fileToUpload, destLocation, force);
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

        public static TokenCredentials AuthenticateUser(string tenantId, string resource, string appClientId, Uri appRedirectUri, string clientId)
        {
            //return new TokenCredentials(GetAccessToken(@"https://login.microsoftonline.com/common"login.windows.net, "https://management.core.windows.net/", "").GetAwaiter().GetResult());
            return new TokenCredentials(GetAccessToken(@"https://login.windows.net/common", "https://management.core.windows.net/", clientId).GetAwaiter().GetResult());
            //return new TokenCredentials(GetAccessToken(@"https://login.windows.net/common", "https://management.core.windows.net/", clientId));
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
            while (i>0)
            {
                try
                {
                    result = await context.AcquireTokenAsync(resource, clientId, new UserPasswordCredential(_adlsUserName, _adlsPin));
                    return result.AccessToken;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    i--;
                }
            }
            return result.AccessToken;
        }

        //private static string GetAccessToken(string authority, string resource, string clientId)
        //{
        //    var context = new AuthenticationContext(
        //       authority,
        //       TokenCache.DefaultShared);
            
        //    var result = context.AcquireToken(resource,clientId,new UserCredential("adlsperf@microsoft.com", "CaboCabo234$"));
        //    return result.AccessToken;

        //}
        // List files and directories
        public static List<FileStatusProperties> ListItems(string directoryPath)
        {
            return _adlsFileSystemClient.FileSystem.ListFileStatus(_adlsAccountName, directoryPath).FileStatuses.FileStatus.ToList();
        }

        //Set up clients
        public static void SetupClients(TokenCredentials tokenCreds, string subscriptionId)
        {
            _adlsClient = new DataLakeStoreAccountManagementClient(tokenCreds);
            _adlsClient.SubscriptionId = subscriptionId;
            
            _adlsFileSystemClient = new DataLakeStoreFileSystemManagementClient(tokenCreds);
        }
    
    }
}