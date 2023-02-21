using MazeService.Dtos;

namespace MazeService.SyncDataServices
{
    public interface IMazeGenDataClient
    {
        Task<int> GenerateMaze(MazeGenerateRequest mazeGenerateRequest);
    }
}