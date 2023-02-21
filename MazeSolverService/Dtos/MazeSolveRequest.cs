using MazeSolverService.Models;

namespace MazeSolverService.Dtos

{
    public class MazeSolveRequest
    {
        public long Id {get;set;}
        public Algorithm algorithm {get;set;}

        public Position start {get; set;}
        public Position end {get; set;}
    }
}