using MazeSolverService.Models;
using MazeSolverService.Solvers;

namespace MazeSolverService.Services
{
    public class MazeSolverServiceImpl : IMazeSolverService
    {
        private readonly Dictionary<Algorithm, IMazeSolver> dispatcher = new Dictionary<Algorithm, IMazeSolver>();
        public MazeSolverServiceImpl()
        {
            dispatcher.Add(Algorithm.BFS, new BFSSolver());
        }

        public MazePath solveMaze(Maze maze, Algorithm algorithm, Position start, Position end)
        {
            return dispatcher[algorithm].solve(maze, start, end);
        }
    }
}