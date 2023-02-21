

namespace MazeSolverService.Models
{
    public class Cell
    {    
        public int posX { get; set; } 
        public int posY { get; set; }
        public bool leftWall { get; set; } 
        public bool upWall { get; set; }     
        public bool rightWall { get; set; } 
        public bool downWall { get; set; } 

        public static Cell initialCell(int posX, int posY)
        {
            return new Cell()
            {
                posX = posX,
                posY = posY,
                leftWall = true,
                upWall = true,
                rightWall = true,
                downWall = true
            };
        }
    }
}