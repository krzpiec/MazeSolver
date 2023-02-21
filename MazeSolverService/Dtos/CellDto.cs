using MazeSolverService.Models;

namespace MazeSolverService.Dtos
{
    public class CellDto
    {
        public int walls {get; set;}


        public Cell toModel(int posX, int posY)
        {
            var wallsConverted = walls.ToString();
            var wallsString = "";
            for(int i=0 ; i < 4- wallsConverted.Length; i++)
            {
                wallsString += "0";
            }
            wallsString += wallsConverted;
            return new Cell()
            {
                posX = posX,
                posY = posY,
                upWall = wallsString[0] == '1' ? true : false,
                rightWall = wallsString[1] == '1' ? true : false,
                downWall = wallsString[2] == '1' ? true : false,
                leftWall = wallsString[3] == '1' ? true : false,
            };
        }
    }
}