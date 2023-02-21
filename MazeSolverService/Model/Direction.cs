namespace MazeSolverService.Models
{
    public enum Direction
    {
        UP,
        RIGHT,
        DOWN,
        LEFT,
        NONE
    }

    public static class DirectionExtension
    {
        public static Direction getOpposite(this Direction other)
        {
            return other switch
            {
                Direction.UP => Direction.DOWN,
                Direction.RIGHT => Direction.LEFT,
                Direction.DOWN => Direction.UP,
                Direction.LEFT => Direction.RIGHT,
            };
        }
    }
}