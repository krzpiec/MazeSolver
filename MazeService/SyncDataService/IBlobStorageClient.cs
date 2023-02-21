
namespace MazeService.SyncDataServices
{
    public interface IBlobStorageClient
    {
        Task<string> GetMazeBlob(long mazeId);
    }
}