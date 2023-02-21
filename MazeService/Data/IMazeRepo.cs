using MazeService.Models;

namespace MazeService.Data
{
    public interface IMazeRepo
    {
        bool saveChanges();

        IEnumerable<Maze> GetAllMazes();

        Maze GetMazeById(long id);

        long CreateMaze(Maze platform);
    }
}