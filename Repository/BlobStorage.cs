
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace StorageAccount.Repository
{
    public class BlobStorage
    {
        static string connectionString="DefaultEndpointsProtocol=https;AccountName=sampathstorageaccount;AccountKey=2se2PjwF3ljSfTRgkOJEwqOUmSj5hlhIYeicMIUBdaQg/PT8MQkH/7WR/Ox1YUVwV4BRJBw45SwE+AStgSTYUw==;EndpointSuffix=core.windows.net";
        public static async Task CreateBlob(string blobName)
        {
            if(string.IsNullOrEmpty(blobName))
            {
                throw new ArgumentNullException("enter blob name");
            }
            try
            {
                BlobContainerClient container=new BlobContainerClient(connectionString,blobName);
                await container.CreateAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public static async Task DeleteBlob(string blobName)
        {
            if(string.IsNullOrEmpty(blobName))
            {
                throw new ArgumentNullException("enter blob name");
            }
            try
            {
                BlobContainerClient container=new BlobContainerClient(connectionString,blobName);
                await container.DeleteAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public static async Task DeleteBlobContent(string blobName,string file)
        {
             if(string.IsNullOrEmpty(blobName))
            {
                throw new ArgumentNullException("enter blob name");
            }
            try
            {
                BlobContainerClient container=new BlobContainerClient(connectionString,blobName);
                await container.DeleteBlobAsync(file);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<BlobProperties> UpdateBlobContent(string blobName,string file)
        {
             if(string.IsNullOrEmpty(blobName))
            {
                throw new ArgumentNullException("enter blob name");
            }
            try
            {
                string fileName=Path.GetTempFileName();
                BlobContainerClient container=new BlobContainerClient(connectionString,blobName);
                BlobClient blob=container.GetBlobClient(file);
                await blob.UploadAsync(fileName);
                BlobProperties prop=await blob.GetPropertiesAsync();
                return prop;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<BlobProperties> getBlobContent(string blobName,string file)
        {
            if(string.IsNullOrEmpty(blobName))
            {
                throw new ArgumentNullException("enter blob name");
            }
            try
            {
                BlobContainerClient container=new BlobContainerClient(connectionString,blobName);
                BlobClient blob=container.GetBlobClient(file);
                BlobProperties prop=await blob.GetPropertiesAsync();
                return prop;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<List<string>> GetBlob(string blobName,string file)
        {
           if(string.IsNullOrEmpty(blobName))
            {
                throw new ArgumentNullException("enter blob name");
            }
            try
            {
                BlobContainerClient container=new BlobContainerClient(connectionString,blobName);
                List<string> names=new List<string>();
                await foreach(BlobItem a in container.GetBlobsAsync())
                {
                    names.Add(a.Name);
                }
                return names;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<BlobProperties> DownloadBlob(string blobName,string file)
        {
            try
            {
                string path=@"C:\Users\vmadmin\Desktop\StorageAccount\Downloads\"+blobName;
                BlobContainerClient container=new BlobContainerClient(connectionString,blobName);
                BlobClient client=container.GetBlobClient(file);
                await client.DownloadToAsync(path);
                BlobProperties prop=await client.GetPropertiesAsync();
                return prop;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}