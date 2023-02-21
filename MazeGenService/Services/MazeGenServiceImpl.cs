using MazeGenService.Dtos;
using MazeGenService.Generators;
using MazeGenService.Models;

namespace MazeGenService.Services
{
    public class MazeGenServiceImpl : IMazeGenService
    {
        public string generateMaze(int SizeX, int SizeY)
        {
            Console.WriteLine("hello");
            IMazeGenerator mazegen = new BacktrancingMazeGenerator(SizeX, SizeY);
            List<List<Cell>> cellsList = mazegen.generateMaze();
           for (int j = 0; j < cellsList[0].Count; j++)
                {
                    if ( cellsList[0][j].upWall == false)
                    {
                        Console.WriteLine("HUGE ERROR");
                    }
                }
            return MazeDto.fromModel(new Maze() { maze = cellsList }).Stringfy();
        }
    }
}