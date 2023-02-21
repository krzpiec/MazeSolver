using Azure.Storage.Blobs.Models;

namespace BlobService.Services
{
    public interface IBlobService
    {
        public Task<string> getBlob(long id);

        public Task<int> uploadBlob(long id, string content);

        public Task<int> deleteBlob(long id);
    }
}