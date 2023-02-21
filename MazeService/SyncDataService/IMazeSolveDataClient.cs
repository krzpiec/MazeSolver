using MazeService.Dtos;
using MazeService.Models;

namespace MazeService.SyncDataServices
{
    public interface IMazeSolveDataClient
    {
        Task<MazePath> SolveMaze(MazeSolveRequest mazeSolveRequest);
    }
}