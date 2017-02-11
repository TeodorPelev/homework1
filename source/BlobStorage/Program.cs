using Microsoft.Azure; // Namespace for CloudConfigurationManager
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Blob; // Namespace for Blob storage types
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Threading;

namespace BlobStorage
{
    class Program
    {
        static void Main(string[] args)
        {
            // 01 - Connect to your azure storage account

            // 02 - Create a container called "text-files"

            // 03 - Set the container permissions to BlobContainerPublicAccessType.Blob

            // 04 - Upload SampleText.txt to a block block called "UploadedSampleText.txt"

            // 05 - Download "UploadedSampleText.txt" from the storage account and prints its contents using Console.WriteLine()

            // 05 - Delete UploadedSampleText.txt from the storage


            // 01 - Connecting to my storage account via the connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Creating the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // 02 - Getting a reference for the container and creating it if it doesn't exist.
            CloudBlobContainer container = blobClient.GetContainerReference("text-files");

            container.CreateIfNotExists();
            // 03 - Setting permissions
            BlobContainerPermissions permissions = new BlobContainerPermissions()
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            };
            container.SetPermissions(permissions);
            
            // 04- Getting a reference to the blob.
            CloudBlockBlob blockBlob = container.GetBlockBlobReference("UploadedSampleText.txt");
            
            // Uploadeing the file to the blob.
            using (var fileStream = System.IO.File.OpenRead(@"..\..\SampleText.txt"))
            {
                blockBlob.UploadFromStream(fileStream);
            }

            //05 - Blob download and output.
            string text;
            using (var memoryStream = new MemoryStream())
            {
                blockBlob.DownloadToStream(memoryStream);
                text = System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
            }
            Console.WriteLine(text);

            // 06 - Deleting the blob.
            //blockBlob.Delete();
            Thread.Sleep(5000);
            
        }
    }
}
