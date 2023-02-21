using System.Text.Json;
using MazeSolverService.Dtos;
using MazeSolverService.Models;
using MazeSolverService.Services;
using MazeSolverService.SyncDataServices;
using Microsoft.AspNetCore.Mvc;

namespace MazeSolverService.Controllers
{
 [Route("api/c")]
    [ApiController]
    public class MazeSolverController : ControllerBase
    {
        private readonly IBlobStorageClient _blobStorageClient;
        private readonly IMazeSolverService _mazeSolverService;

        public MazeSolverController(IBlobStorageClient blobStorageClient, IMazeSolverService mazeSolverService)
        {
            _blobStorageClient = blobStorageClient;
            _mazeSolverService = mazeSolverService;
        }


        [HttpPost("solve", Name = "SolveMaze")]
        public async Task<ActionResult<MazePath>> SolveMaze(MazeSolveRequest mazeSolveRequest)
        {
            string mazeBlob = await _blobStorageClient.GetMazeBlob(mazeSolveRequest.Id);
            List<List<CellDto>> mazeCells =  JsonSerializer.Deserialize<List<List<CellDto>>>(mazeBlob);
            MazeDto mazeDto = new MazeDto(){cells = mazeCells};
            Maze mazeModel = mazeDto.toModel();
            return _mazeSolverService.solveMaze(mazeModel, mazeSolveRequest.algorithm, mazeSolveRequest.start, mazeSolveRequest.end);
        }
    }
}