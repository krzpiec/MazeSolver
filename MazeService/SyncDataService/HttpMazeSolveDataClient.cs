using System.Text;
using System.Text.Json;
using MazeService.Dtos;

namespace MazeService.SyncDataServices
{
    public class HttpMazeSolveDataClient : IMazeSolveDataClient
    {
        private readonly HttpClient _httpCLient;
        private IConfiguration _configuration;

        public HttpMazeSolveDataClient(HttpClient htppClient, IConfiguration configuration)
        {
            _httpCLient = htppClient;
            _configuration = configuration;
        }

        async Task<MazePath> IMazeSolveDataClient.SolveMaze(MazeSolveRequest mazeSolveRequest)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(mazeSolveRequest),
                Encoding.UTF8,
                "application/json"
            );

            var url =  $"{_configuration["MazeSolveServiceUrl"]}/api/c/solve";
            Console.WriteLine(url);
            var response = await _httpCLient.PostAsync(
                url,
                httpContent
            );
             var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<MazePath>(content); 
        }
    }
}