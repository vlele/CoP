using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaService
{
    public class MediaDataService
    {
        private const string connectionString = "";
        private const string containerName = "images";
        private const string defaultImage = "image.jpg";

        public static byte[] GetImageAsByteArray(string handle)
        {
            CloudStorageAccount storageAcct = CloudStorageAccount.Parse(connectionString);
            CloudBlobClient blobClient = storageAcct.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);

            string imageName = String.Concat(handle, ".jpg");
            CloudBlockBlob blob = container.GetBlockBlobReference(imageName);
            if (!blob.Exists()) blob = container.GetBlockBlobReference(defaultImage);

            blob.FetchAttributes();
            long fileLength = blob.Properties.Length;
            byte[] fileBytes = new byte[fileLength];

            for (long i = 0; i < fileLength; i++)
            {
                fileBytes[i] = 0x20;
            }

            blob.DownloadToByteArray(fileBytes, 0);
            return fileBytes;
        }
    }
}
