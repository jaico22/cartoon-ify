using Infastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Win32.SafeHandles;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Infastructure.Azure
{
    public class AzureFileUploader : IFileUploader
    {
        private readonly SecretRetriever _secretRetriever = new SecretRetriever();

        public async Task<string> UploadAsync(string fileName, Stream fileStream)
        {
            fileName += Guid.NewGuid() + ".png";

            var connectionString = _secretRetriever.GetBlobStorageConnectionString();
            // Check whether the connection string can be parsed.
            if (CloudStorageAccount.TryParse(connectionString, out CloudStorageAccount storageAccount))
            {
                // If the connection string is valid, proceed with operations against Blob
                // storage here.
                CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();

                // Create a file in your local MyDocuments folder to upload to a blob.

                CloudBlobContainer cloudBlobContainer =
                    cloudBlobClient.GetContainerReference("processedimages");

                // Get a reference to the blob address, then upload the file to the blob.
                // Use the value of localFileName for the blob name.
                CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(fileName);
                await cloudBlockBlob.UploadFromStreamAsync(fileStream);

                // Return URI of file name
                return fileName;
            }
            else
            {
                // Otherwise, let the user know that they need to define the environment variable.
                Console.WriteLine(
                    "A connection string has not been defined in the system environment variables. " +
                    "Add an environment variable named 'CONNECT_STR' with your storage " +
                    "connection string as a value.");
                Console.WriteLine("Press any key to exit the application.");
                Console.ReadLine();
                return null;
            }
        }

    }
}
