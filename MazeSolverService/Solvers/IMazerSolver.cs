using MazeSolverService.Models;

namespace MazeSolverService.Solvers
{
    public interface IMazeSolver
    {
        MazePath solve(Maze maze, Position start, Position end); 
    }
}