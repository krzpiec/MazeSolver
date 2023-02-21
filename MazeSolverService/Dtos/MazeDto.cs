using System.Text.Json;
using MazeSolverService.Models;

namespace MazeSolverService.Dtos
{
    public class MazeDto
    {
        public List<List<CellDto>> cells;

        public Maze toModel()
        {
            int SizeX = cells.Count();
            int SizeY = cells.First().Count();

            List<List<Cell>> mazeCells = new List<List<Cell>>();
            for(int row = 0; row < SizeX; row++)
            {
                List<CellDto> dtoColumnCells = cells[row];
                List<Cell> modelColumnCells = new List<Cell>();           
                for(int column = 0; column < SizeY; column++)
                {
                    modelColumnCells.Add(dtoColumnCells[column].toModel(row,column));
                }
                mazeCells.Add(modelColumnCells);
            }
            return new Maze(){maze = mazeCells};
        }
    }
}