

namespace MazeSolverService.SyncDataServices
{
    public interface IBlobStorageClient
    {
        Task<string> GetMazeBlob(long mazeId);
    }
}