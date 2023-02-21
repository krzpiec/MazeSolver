using Microsoft.AspNetCore.Mvc;
using MazeGenService.Dtos;
using MazeGenService.SyncDataServices;
using MazeGenService.Services;

namespace MazeGenService.Controllers
{
    [Route("api/c")]
    [ApiController]
    public class MazesGenController : ControllerBase
    {
        private readonly IBlobStorageClient _blobStorageClient;
        private readonly IMazeGenService _mazeGenService;

        public MazesGenController(IBlobStorageClient blobStorageClient, IMazeGenService mazeGenService)
        {
            _blobStorageClient = blobStorageClient;
            _mazeGenService = mazeGenService;
        }


        [HttpPost("generate", Name = "GenerateMaze")]
        public async Task<ActionResult<int>> CreateMaze(MazeGenerateRequest mazeGenerateRequest)
        {
            Console.WriteLine("Weszlismy");
            string stringfiedMaze = _mazeGenService.generateMaze(mazeGenerateRequest.SizeX, mazeGenerateRequest.SizeY);
            Console.WriteLine(mazeGenerateRequest.Id);
            await _blobStorageClient.SaveGeneratedMaze(new GeneratedMazeDto() { Id = mazeGenerateRequest.Id, Content = stringfiedMaze });
            return 1;
        }
    }
}