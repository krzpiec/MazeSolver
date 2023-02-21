using MazeGenService.Models;

namespace MazeGenService.Dtos
{
    public class CellDto
    {
        public int walls {get; set;}


        public Cell toModel(int posX, int posY)
        {
            return new Cell()
            {
                posX = posX,
                posY = posY,
                upWall = walls >> 4 == 1 ? true : false,
                rightWall = walls >> 3 == 1 ? true : false,
                downWall = walls >> 2== 1 ? true : false,
                leftWall = walls >> 1 == 1 ? true : false,
            };
        }

        public static CellDto formModel(Cell cell)
        {
            string up = cell.upWall?"1":"0";
            string right = cell.rightWall?"1":"0";
            string down = cell.downWall?"1":"0";
            string left = cell.leftWall?"1":"0";
            string wallsString = up + right + down + left;

            return new CellDto(){walls = Int32.Parse(wallsString)};
        }
    }
}