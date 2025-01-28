using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Well_AI.Advisor.Log.Error;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.Function.Notification;

namespace WellAI.Advisor.Helper
{
    public static class AzureBlobStorage
    {
        private static string _readmeFileName = "readmeforwellazureblobstorage.readme";
        public static async Task<bool> EnsureBlobContainerForTenant(string account, string key, string containerNamePrefix, string tenantId)
        {
            string containerName = containerNamePrefix + tenantId;
            var storageCredentials = new StorageCredentials(account, key);
            var cloudStorageAccount = new CloudStorageAccount(storageCredentials, true);
            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();

            var container = cloudBlobClient.GetContainerReference(containerName);
            return await container.CreateIfNotExistsAsync();
        }

        public static async Task<object> UploadFileToBlobContainer(string account, string key, string containerNamePrefix, string tenantId, IFormFile file, string folder)
        {
            string containerName = containerNamePrefix + tenantId;
            var storageCredentials = new StorageCredentials(account, key);
            var cloudStorageAccount = new CloudStorageAccount(storageCredentials, true);
            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();

            var container = cloudBlobClient.GetContainerReference(containerName);
            var db = new WellAINotificationHandlerContext();
            CloudBlockBlob newBlob;

            if(string.IsNullOrEmpty(folder))
                newBlob = container.GetBlockBlobReference(file.FileName);
            else
                newBlob = container.GetBlockBlobReference(folder + "/" + file.FileName);

            using (var fileStream = file.OpenReadStream())
            {
                try
                {
                    await newBlob.UploadFromStreamAsync(fileStream);

                }
                catch (Exception ex)
                {
                    ErrorHandlerForNotification customErrorHandler = new ErrorHandlerForNotification(db);
                    customErrorHandler.WriteError(ex, "AzureBlobStorage UploadFileToBlobContainer", null);
                    throw;
                }
            }

            return new
            {
                name = newBlob.Name,
                uri = newBlob.Uri,
                size = newBlob.Properties.Length
            };
        }
        public static async Task<object> UploadFileToBlobContainerWithFileName(string account, string key, string containerNamePrefix, string tenantId, IFormFile file, string folder, string filename)
        {
            string containerName = containerNamePrefix + tenantId;
            var storageCredentials = new StorageCredentials(account, key);
            var cloudStorageAccount = new CloudStorageAccount(storageCredentials, true);
            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();

            filename = $"{filename}{System.IO.Path.GetExtension(file.FileName)}";
            var db = new WellAINotificationHandlerContext();
            var container = cloudBlobClient.GetContainerReference(containerName);

            string folderpath = "";
            if (string.IsNullOrEmpty(folder))
            {
                folderpath = "filename";
            }
            else
            {
                folderpath = folder + "/" + filename;
            }
                
            CloudBlockBlob newBlob;

            if (string.IsNullOrEmpty(folder))
                newBlob = container.GetBlockBlobReference(filename);
            else
                newBlob = container.GetBlockBlobReference(folder + "/" + filename);

            //var blobContainer = container.GetBlockBlobReference("graphs-data");
            if (newBlob.ExistsAsync().Result!=true)
            {
                newBlob.Container.CreateAsync().Wait();
            }

            using (var fileStream = file.OpenReadStream())
            {
                try
                {
                    await newBlob.UploadFromStreamAsync(fileStream);

                }
                catch (Exception ex)
                {
                    ErrorHandlerForNotification customErrorHandler = new ErrorHandlerForNotification(db);
                    customErrorHandler.WriteError(ex, "AzureBlobStorage UploadFileToBlobContainerWithFileName", null);
                    throw;
                }
            }

            return new
            {
                name = newBlob.Name,
                uri = newBlob.Uri,
                size = newBlob.Properties.Length
            };
        }

        public static async Task EnsureFoldersInContainerForTenant(string account, string key, string containerNamePrefix, string tenantId, List<string> folderNames)
        {
            string containerName = containerNamePrefix + tenantId;
            var storageCredentials = new StorageCredentials(account, key);
            var cloudStorageAccount = new CloudStorageAccount(storageCredentials, true);
            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();

            var container = cloudBlobClient.GetContainerReference(containerName);

            foreach(var folderName in folderNames)
            {
                var newBlob = container.GetBlockBlobReference(folderName + "/" + _readmeFileName);
                using (var fileStream = Utils.GenerateStreamFromString("This file is for folder storage. Please dont remove it."))
                {
                    await newBlob.UploadFromStreamAsync(fileStream);
                }
            }
        }
        public static async Task CreateNewFolderInContainerForTenant(string account, string key, string containerNamePrefix, string tenantId, string folderPath)
        {
            string containerName = containerNamePrefix + tenantId;
            var storageCredentials = new StorageCredentials(account, key);
            var cloudStorageAccount = new CloudStorageAccount(storageCredentials, true);
            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();

            var container = cloudBlobClient.GetContainerReference(containerName);

            var newBlob = container.GetBlockBlobReference(folderPath + "/" + _readmeFileName);
            using (var fileStream = Utils.GenerateStreamFromString("This file is for folder storage. Please dont remove it."))
            {
                await newBlob.UploadFromStreamAsync(fileStream);
            }
        }

        public static async Task RenameFolderInContainer(string account, string key, string containerNamePrefix, string tenantId, string oldfolderPath, string newFolderName)
        {
            string containerName = containerNamePrefix + tenantId;
            var storageCredentials = new StorageCredentials(account, key);
            var cloudStorageAccount = new CloudStorageAccount(storageCredentials, true);
            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();

            var container = cloudBlobClient.GetContainerReference(containerName);

            var oldPaths = oldfolderPath.Split('/', StringSplitOptions.RemoveEmptyEntries);
            var newpath = "";

            for(var i = 0; i < oldPaths.Length - 1; i++)
            {
                newpath += oldPaths[i] + "/";
            }

            newpath += newFolderName;

            var newBlob = container.GetBlockBlobReference(newpath);
            if(await newBlob.ExistsAsync()) // cannot rename old folder to name of another existing folder
            {
                return;
            }

            IEnumerable<IListBlobItem> items = null;
            BlobContinuationToken blobContinuationToken = null;

            CloudBlobDirectory sourceDirectory = container.GetDirectoryReference(oldfolderPath);

            do
            {
                var resultSegment = await sourceDirectory.ListBlobsSegmentedAsync(
                    useFlatBlobListing: true,
                    blobListingDetails: BlobListingDetails.Metadata,
                    maxResults: null,
                    currentToken: blobContinuationToken,
                    options: null,
                    operationContext: null
                );

                blobContinuationToken = resultSegment.ContinuationToken;

                if (blobContinuationToken == null)
                {
                    items = resultSegment.Results;//.Where(x => !x.Uri.AbsoluteUri.EndsWith(_readmeFileName));
                }

            } while (blobContinuationToken != null);

            foreach(var item in items)
            {
                var blob = (CloudBlob)item;
                var foldersplit = blob.Name.Split('/', StringSplitOptions.RemoveEmptyEntries);

                var targetBlob = container.GetBlobReference(newpath + "/" + foldersplit[foldersplit.Length - 1]);

                await targetBlob.StartCopyAsync(blob.Uri);
            }

            var oldblob = container.GetBlockBlobReference(oldfolderPath);
            //await oldblob.DeleteIfExistsAsync();

            /*var sourceContainer = cloudBlobClient.GetContainerReference("source-container");
            var targetContainer = cloudBlobClient.GetContainerReference("target-container");
            targetContainer.CreateIfNotExists();//Create target container
            BlobContinuationToken continuationToken = null;
            do
            {
                Console.WriteLine("Listing blobs. Please wait...");
                var blobsResult = sourceContainer.ListBlobsSegmented(prefix: "", useFlatBlobListing: true, blobListingDetails: BlobListingDetails.All, maxResults: 1000, currentToken: continuationToken, options: new BlobRequestOptions(), operationContext: new OperationContext());
                continuationToken = blobsResult.ContinuationToken;
                var items = blobsResult.Results;
                foreach (var item in items)
                {
                    var blob = (CloudBlob)item;
                    var targetBlob = targetContainer.GetBlobReference(blob.Name);
                    Console.WriteLine(string.Format("Copying \"{0}\" from \"{1}\" blob container to \"{2}\" blob container.", blob.Name, sourceContainer.Name, targetContainer.Name));

                    targetBlob.StartCopy(blob.Uri);
                }
            } while (continuationToken != null);
            Console.WriteLine("Deleting source blob container. Please wait.");
            //sourceContainer.DeleteIfExists();
            Console.WriteLine("Rename container operation complete. Press any key to terminate the application.");*/
        }

        public static async Task EnsureFolderTreeInContainerForTenant(string account, string key, string containerNamePrefix, string tenantId, List<WellFileFolder> folders)
        {
            string containerName = containerNamePrefix + tenantId;
            var storageCredentials = new StorageCredentials(account, key);
            var cloudStorageAccount = new CloudStorageAccount(storageCredentials, true);
            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();

            var container = cloudBlobClient.GetContainerReference(containerName);

            var rootFolders = folders.Where(x => string.IsNullOrEmpty(x.Parent)).ToList();

            foreach (var rootFolder in rootFolders)
            {
                var newBlob = container.GetBlockBlobReference(rootFolder.FolderName + "/" + _readmeFileName);
                using (var fileStream = Utils.GenerateStreamFromString("This file is for folder storage. Please dont remove it."))
                {
                    await newBlob.UploadFromStreamAsync(fileStream);
                }

                var childFolders = folders.Where(x => x.Parent == rootFolder.Id).ToList();

                foreach(var childFolder in childFolders)
                {
                    var newcBlob = container.GetBlockBlobReference(rootFolder.FolderName + "/" + childFolder.FolderName + "/" + _readmeFileName);
                    using (var fileStream = Utils.GenerateStreamFromString("This file is for folder storage. Please dont remove it."))
                    {
                        await newcBlob.UploadFromStreamAsync(fileStream);
                    }
                }
            }
        }

        public static async Task EnsureAndCheckFolderTreeInContainerForTenant(string account, string key, string containerNamePrefix, string tenantId, List<WellFileFolder> folders)
        {
            string containerName = containerNamePrefix + tenantId;
            var storageCredentials = new StorageCredentials(account, key);
            var cloudStorageAccount = new CloudStorageAccount(storageCredentials, true);
            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();

            var container = cloudBlobClient.GetContainerReference(containerName);

            var rootFolders = folders.Where(x => string.IsNullOrEmpty(x.Parent)).ToList();
            var db = new WellAINotificationHandlerContext();
            foreach (var rootFolder in rootFolders)
            {
                var blobExist = true;

                CloudBlockBlob newBlob = null;

                try
                {
                    newBlob = container.GetBlockBlobReference(rootFolder.FolderName + "/" + _readmeFileName);

                    blobExist = await newBlob.ExistsAsync();
                }
                catch(Exception e)
                {
                    ErrorHandlerForNotification customErrorHandler = new ErrorHandlerForNotification(db);
                    customErrorHandler.WriteError(e, "AzureBlobStorage EnsureAndCheckFolderTree", null);
                    blobExist = false;
                }
                finally
                {
                    if (!blobExist)
                    {
                        using (var fileStream = Utils.GenerateStreamFromString("This file is for folder storage. Please dont remove it."))
                        {
                            await newBlob.UploadFromStreamAsync(fileStream);
                        }
                    }
                }

                var childFolders = folders.Where(x => x.Parent == rootFolder.Id).ToList();

                foreach (var childFolder in childFolders)
                {
                    var newcBlob = container.GetBlockBlobReference(rootFolder.FolderName + "/" + childFolder.FolderName + "/" + _readmeFileName);

                    blobExist = true;

                    try
                    {
                        blobExist = await newcBlob.ExistsAsync();
                    }
                    catch(Exception ex)
                    {
                        ErrorHandlerForNotification customErrorHandler = new ErrorHandlerForNotification(db);
                        customErrorHandler.WriteError(ex, "AzureBlobStorage EnsureAndCheckFolderTreeInContainerForTenant", null);
                        blobExist = false;
                    }
                    finally
                    {
                        if (!blobExist)
                        {
                            using (var fileStream = Utils.GenerateStreamFromString("This file is for folder storage. Please dont remove it."))
                            {
                                await newcBlob.UploadFromStreamAsync(fileStream);
                           }
                        }
                    }
                }
            }
        }

        public static async Task<Dictionary<string, List<IListBlobItem>>> GetFilesBlobContainer(string account, string key, string containerNamePrefix, string tenantId, string folder)
        {
            string containerName = containerNamePrefix + tenantId;
            var storageCredentials = new StorageCredentials(account, key);
            var cloudStorageAccount = new CloudStorageAccount(storageCredentials, true);
            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();

            var container = cloudBlobClient.GetContainerReference(containerName);

            IEnumerable<IListBlobItem> items = null;
            var files = new Dictionary<string, List<IListBlobItem>>();// directory->files in it
            var dirFiles = new List<IListBlobItem>();

            BlobContinuationToken blobContinuationToken = null;
            if (string.IsNullOrEmpty(folder))
            {
                do
                {
                    var resultSegment = await container.ListBlobsSegmentedAsync(
                        prefix: null,
                        useFlatBlobListing: false,
                        blobListingDetails: BlobListingDetails.Metadata,
                        maxResults: null,
                        currentToken: blobContinuationToken,
                        options: null,
                        operationContext: null
                    );

                    // Get the value of the continuation token returned by the listing call.
                    blobContinuationToken = resultSegment.ContinuationToken;

                    if (blobContinuationToken == null)
                    {
                        items = resultSegment.Results.Where(x => !x.Uri.AbsoluteUri.EndsWith(_readmeFileName));
                    }

                } while (blobContinuationToken != null);
            }
            else
            {
                CloudBlobDirectory directory = container.GetDirectoryReference(folder);

                do
                {
                    var resultSegment = await directory.ListBlobsSegmentedAsync(
                        useFlatBlobListing: false,
                        blobListingDetails: BlobListingDetails.Metadata,
                        maxResults: null,
                        currentToken: blobContinuationToken,
                        options: null,
                        operationContext: null
                    );

                    // Get the value of the continuation token returned by the listing call.
                    blobContinuationToken = resultSegment.ContinuationToken;

                    if (blobContinuationToken == null)
                    {
                        items = resultSegment.Results;//.Where(x => !x.Uri.AbsoluteUri.EndsWith(_readmeFileName));
                    }

                } while (blobContinuationToken != null);
            }

            foreach(var item in items)
            {
                var cloudDir = item as CloudBlobDirectory;
                if (cloudDir == null) {
                    dirFiles.Add(item);
                }
                else if (cloudDir != null) {

                    var path = item.Uri.PathAndQuery.Replace(containerName, "");
                    var pathOrigin = path;

                    if(item.Parent != null)
                    {
                        var parentPath = item.Parent.Uri.PathAndQuery.Replace(containerName, "");

                        path = path.Replace(parentPath, "").Replace("/", "");

                        if(path.Trim('/') == "" && pathOrigin.Trim('/') == parentPath.Trim('/'))
                        {
                            path = parentPath.Trim('/');
                        }
                    }

                    blobContinuationToken = null;
                    do
                    {
                        var resultSegment = await cloudDir.ListBlobsSegmentedAsync(
                            useFlatBlobListing: true,
                            blobListingDetails: BlobListingDetails.Metadata,
                            maxResults: null,
                            currentToken: blobContinuationToken,
                            options: null,
                            operationContext: null
                        );

                        // Get the value of the continuation token returned by the listing call.
                        blobContinuationToken = resultSegment.ContinuationToken;

                        if (blobContinuationToken == null)
                        {
                            files.Add(path,
                                resultSegment.Results.ToList());//.Where(x => !x.Uri.AbsoluteUri.EndsWith(_readmeFileName)).ToList());
                        }

                    } while (blobContinuationToken != null);
                }
            }

            // all root files inside directory
            if (!string.IsNullOrEmpty(folder) && dirFiles.Count > 0)
                files.Add(folder, dirFiles);

            return files;
        }

        public static async Task<KeyValuePair<string, byte[]>> DownloadFilesFromBlobContainer(string account, string key, string containerNamePrefix, string tenantId, string path)
        {
            string containerName = containerNamePrefix + tenantId;
            var storageCredentials = new StorageCredentials(account, key);
            var cloudStorageAccount = new CloudStorageAccount(storageCredentials, true);
            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();

            var container = cloudBlobClient.GetContainerReference(containerName);

            var split = path.Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
            if (split.Length < 2)
                return new KeyValuePair<string, byte[]>();

            var fileName = split[split.Length - 1];
            var folder = path.Replace(fileName, "").Replace("%20", " ");
            
            if (string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(folder) || !fileName.Contains("."))
                return new KeyValuePair<string, byte[]>();

            CloudBlobDirectory directory = container.GetDirectoryReference(folder);
            var fileblob = directory.GetBlockBlobReference(fileName);

            byte[] temp = null;

            using (var ms = new MemoryStream())
            {
                await fileblob.DownloadToStreamAsync(ms);

                await ms.FlushAsync();
                ms.Position = 0;
                temp = new byte[ms.Length];

                await ms.ReadAsync(temp, 0, (int)ms.Length);
            }

            var result = new KeyValuePair<string, byte[]>(fileblob.Properties.ContentType, temp);

            return result;
        }

        public static async Task<KeyValuePair<string, byte[]>> DownloadFilesFromBlobBlobContainer(string account, string key, string containerNamePrefix, string tenantId, string blobBlob,string fileName)
        {
            string containerName = containerNamePrefix + tenantId;
            var storageCredentials = new StorageCredentials(account, key);
            var cloudStorageAccount = new CloudStorageAccount(storageCredentials, true);
            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();

            var container = cloudBlobClient.GetContainerReference(containerName);

            if (string.IsNullOrEmpty(fileName))
                return new KeyValuePair<string, byte[]>();

            if (string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(blobBlob))
                return new KeyValuePair<string, byte[]>();

            CloudBlobDirectory directory = container.GetDirectoryReference(blobBlob);
            var fileblob = directory.GetBlockBlobReference(fileName);

            byte[] temp = null;

            using (var ms = new MemoryStream())
            {
                await fileblob.DownloadToStreamAsync(ms);

                await ms.FlushAsync();
                ms.Position = 0;
                temp = new byte[ms.Length];

                await ms.ReadAsync(temp, 0, (int)ms.Length);
            }

            var result = new KeyValuePair<string, byte[]>(fileblob.Properties.ContentType, temp);

            return result;
        }

        public static async Task<bool> Delete(string account, string key, string containerNamePrefix, string tenantId, string path, string fileName)
        {
            string containerName = containerNamePrefix + tenantId;
            var storageCredentials = new StorageCredentials(account, key);
            var cloudStorageAccount = new CloudStorageAccount(storageCredentials, true);
            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();

            var container = cloudBlobClient.GetContainerReference(containerName);

            CloudBlobDirectory directory = container.GetDirectoryReference(path);
            var fileblob = directory.GetBlockBlobReference(fileName);

            return await fileblob.DeleteIfExistsAsync();
        }

        public static async Task<bool> DeleteFileByPath(string account, string key, string containerNamePrefix, string tenantId, string path)
        {
            string containerName = containerNamePrefix + tenantId;
            var storageCredentials = new StorageCredentials(account, key);
            var cloudStorageAccount = new CloudStorageAccount(storageCredentials, true);
            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();

            var container = cloudBlobClient.GetContainerReference(containerName);

            var fileblob = container.GetBlockBlobReference(path);

            return await fileblob.DeleteIfExistsAsync();
        }

        public static async Task<List<KeyValuePair<string, string>>> DeleteFolder(string account, string key, string containerNamePrefix, string tenantId, string path)
        {
            var result = new List<KeyValuePair<string, string>>();

            string containerName = containerNamePrefix + tenantId;
            var storageCredentials = new StorageCredentials(account, key);
            var cloudStorageAccount = new CloudStorageAccount(storageCredentials, true);
            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();

            var container = cloudBlobClient.GetContainerReference(containerName);

            CloudBlobDirectory directory = container.GetDirectoryReference(path);

            result = await DeleteFolderFiles(containerName, directory);

            return result;
        }

        private static async Task<List<KeyValuePair<string, string>>> DeleteFolderFiles(string containerName, CloudBlobDirectory directory)
        {
            var result = new List<KeyValuePair<string, string>>();

            BlobContinuationToken blobContinuationToken = null;
            IEnumerable<IListBlobItem> items = null;
            var files = new List<IListBlobItem>();

            do
            {
                var resultSegment = await directory.ListBlobsSegmentedAsync(
                    useFlatBlobListing: false,
                    blobListingDetails: BlobListingDetails.Metadata,
                    maxResults: null,
                    currentToken: blobContinuationToken,
                    options: null,
                    operationContext: null
                );

                // Get the value of the continuation token returned by the listing call.
                blobContinuationToken = resultSegment.ContinuationToken;

                if (blobContinuationToken == null)
                {
                    items = resultSegment.Results;
                }

            } while (blobContinuationToken != null);

            foreach (var item in items)
            {
                var cloudDir = item as CloudBlobDirectory;
                if (cloudDir == null)
                {
                    files.Add(item);
                }
                else
                {
                    var tempres = await DeleteFolderFiles(containerName, cloudDir);

                    result.AddRange(tempres);
                }
            }

            // deleting files after all folders were iterated because folder is not exists without files
            foreach(var file in files)
            {
                var parPath = file.Parent.Uri.PathAndQuery;

                var fName = file.Uri.PathAndQuery.Replace(parPath, "").Replace("%20", " ");

                var fileblob = file as CloudBlockBlob;

                if (fName != _readmeFileName)
                {
                    result.Add(new KeyValuePair<string, string>(fName, fileblob.Name));
                }

                await fileblob.DeleteIfExistsAsync();
            }

            return result;
        }

        public static async Task<string> GetFileBlobContainer(string account, string key, string containerNamePrefix, string tenantId,string folder,string fileName)
        {

            
            string containerName = containerNamePrefix + tenantId;

            var storageCredentials = new StorageCredentials(account, key);
            var cloudStorageAccount = new CloudStorageAccount(storageCredentials, true);
            CloudBlobClient blobClient= cloudStorageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);
            CloudBlobDirectory directory = container.GetDirectoryReference(folder);

            //Phase II Changes - 03/27/2021
            if(fileName!=null)
            {
                CloudBlockBlob blockBlob = directory.GetBlockBlobReference(fileName);

                var sharedAccessBlobPolicy = new SharedAccessBlobPolicy
                {
                    Permissions = SharedAccessBlobPermissions.Read,
                    SharedAccessExpiryTime = DateTime.Now.AddDays(1)
                };
                var sasToken = blockBlob.GetSharedAccessSignature(sharedAccessBlobPolicy);
                return blockBlob.Uri + sasToken;
            }
            else
            {
                return await Task.FromResult("");
            }
           
        }
        //Phase II Changes - 03/03/2021
        public static async Task<bool> IsFileExist(string account, string key, string containerNamePrefix, string tenantId, string path)
        {
            string containerName = containerNamePrefix + tenantId;
            var storageCredentials = new StorageCredentials(account, key);
            var cloudStorageAccount = new CloudStorageAccount(storageCredentials, true);
            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();

            var container = cloudBlobClient.GetContainerReference(containerName);

            var fileblob =  container.GetBlockBlobReference(path);
            if (await fileblob.ExistsAsync())
            {
                return true;
            }
            else
            {
                return false;
            }          
        }
    }
}
