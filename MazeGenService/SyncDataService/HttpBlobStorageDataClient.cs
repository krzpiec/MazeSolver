using System.Text;
using System.Text.Json;
using MazeGenService.Dtos;

namespace MazeGenService.SyncDataServices
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

        public async Task SaveGeneratedMaze(GeneratedMazeDto maze)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(maze),
                Encoding.UTF8,
                "application/json"
            );

            var url = $"{_configuration["BlobStorageServiceUrl"]}/api/upload";
            var response = await _httpCLient.PostAsync(url, httpContent);
            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseContent);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("---- Blob service was OK");
            }
            else
            {
                Console.WriteLine("---- Blob service FAILED");
            }
        }
    }
}