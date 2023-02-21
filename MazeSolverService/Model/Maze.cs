using System.Text.Json;

namespace MazeSolverService.Models
{
    public class Maze
    {
        public List<List<Cell>> maze { get; set; }
        
        public static Maze initialMaze(int SizeX, int SizeY)
        {
            List<List<Cell>> mazeCellList = new List<List<Cell>>();
            for (int row = 0; row < SizeX; row++)
            {
                List<Cell> columnCells = new List<Cell>();
                for (int column = 0; column < SizeY; column++)
                {
                    columnCells.Add(Cell.initialCell(row, column));
                }
                mazeCellList.Add(columnCells);
            }
            
            return new Maze() { maze = mazeCellList };
        }
    }
}