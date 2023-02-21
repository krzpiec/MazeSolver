
using MazeGenService.Dtos;

namespace MazeGenService.SyncDataServices
{
    public interface IBlobStorageClient
    {
        Task SaveGeneratedMaze(GeneratedMazeDto generatedMaze);
    }
}