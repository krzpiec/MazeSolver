using System.Text;
using System.Text.Json;

namespace MazeSolverService.SyncDataServices
{
     public class HttpBlobStorageDataClient : IBlobStorageClient
    {
        private readonly HttpClient _httpCLient;
        private IConfiguration _configuration;

        public HttpBlobStorageDataClient(HttpClient htppClient, IConfiguration configuration)
        {
            _httpCLient = htppClient;
            _configuration = configuration;
        }
        public async Task<string> GetMazeBlob(long mazeId)
        {
            var url = $"{_configuration["BlobStorageServiceUrl"]}/api/" + mazeId;
            var response = await _httpCLient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("---- Blob service was OK");
            }
            else
            {
                Console.WriteLine("---- Blob service FAILED");
            }

            return content;
        }
    }
}