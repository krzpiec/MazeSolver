using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace BlobService.Services
{
    public class BlobServiceImpl : IBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;
        public BlobServiceImpl(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task<int> deleteBlob(long id)
        {
            string blobName = id + ".txt";
            var contrainerClient = _blobServiceClient.GetBlobContainerClient("mazes");
            var blobClient = contrainerClient.GetBlobClient(blobName);
            var response = await blobClient.DeleteAsync();
            return response.Status;
        }

        public async Task<string> getBlob(long id)
        {
            string blobName = id + ".txt";
            var contrainerClient = _blobServiceClient.GetBlobContainerClient("mazes");
            var blobClient = contrainerClient.GetBlobClient(blobName);
            BlobDownloadResult downloadResult = await blobClient.DownloadContentAsync();
            return downloadResult.Content.ToString();
        }

        public async Task<int> uploadBlob(long id, string content)
        {
            var blobName = id + ".txt";
            var contrainerClient = _blobServiceClient.GetBlobContainerClient("mazes");
            var blobClient = contrainerClient.GetBlobClient(blobName);
            var response = await blobClient.UploadAsync(BinaryData.FromString(content), overwrite: true);
            return response.GetRawResponse().Status;
        }
    }
}