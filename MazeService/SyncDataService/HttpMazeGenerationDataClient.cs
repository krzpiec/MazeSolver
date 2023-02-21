using System.Text;
using System.Text.Json;
using MazeService.Dtos;

namespace MazeService.SyncDataServices
{
    public class HttpMazeGenDataClient : IMazeGenDataClient
    {
        private readonly HttpClient _httpCLient;
        private IConfiguration _configuration;

        public HttpMazeGenDataClient(HttpClient htppClient, IConfiguration configuration)
        {
            _httpCLient = htppClient;
            _configuration = configuration;
        }

        public async Task<int> GenerateMaze(MazeGenerateRequest mazeGenerateRequest)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(mazeGenerateRequest),
                Encoding.UTF8,
                "application/json"
            );

            var url =  $"{_configuration["MazeGenServiceUrl"]}/api/c/generate";
            Console.WriteLine(url);
            var response = await _httpCLient.PostAsync(
                url,
                httpContent
            );

            if(response.IsSuccessStatusCode)
            {
                Console.WriteLine("---- Maze Gen service was OK");
                return 1;
            }
            else
            {
                 Console.WriteLine("---- Maze Gen service FAILED");
                   return 0;
            }
        }
    }
}