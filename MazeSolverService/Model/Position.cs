namespace MazeSolverService.Models
{
    public class Position
    {
        public int x {get; set;}
        public int y {get; set;}
       
        public bool equals(Position other)
        {
            return x==other.x && y == other.y;
        }

        public bool isDirectionValid(Direction direction, int contextSizeX, int contextSizeY)
        {
            return direction switch
            {
                Direction.UP => x > 0,
                Direction.RIGHT => y < contextSizeY -1,
                Direction.DOWN => x < contextSizeX -1 ,
                Direction.LEFT => y > 0
            };
        }

        public Position getPositionInDirection(Direction direction)
        {
             return direction switch
            {
                Direction.UP => new Position(){x = this.x - 1, y = this.y},
                Direction.RIGHT => new Position(){x = this.x, y = this.y + 1},
                Direction.DOWN => new Position(){x = this.x + 1, y = this.y},
                Direction.LEFT => new Position(){x = this.x, y = this.y - 1}
            };
        }
    }
}