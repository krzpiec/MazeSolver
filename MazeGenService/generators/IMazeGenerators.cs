using MazeGenService.Models;

namespace MazeGenService.Generators
{
    public interface IMazeGenerator
    {
        List<List<Cell>> generateMaze();
    }
}