using MazeSolverService.Models;

namespace MazeSolverService.Services
{
    public interface IMazeSolverService
    {
        MazePath solveMaze(Maze maze, Algorithm algorithm, Position start, Position end);
    }
}