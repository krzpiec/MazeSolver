namespace MazeService.Dtos
{
    public class MazePath
    {
        public PathFindingStatus status {get;set;}
        public List<Position> path {get; set;}
    }
}