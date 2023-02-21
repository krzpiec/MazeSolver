using MazeSolverService.Models;

namespace MazeSolverService.Solvers
{
    public class BFSSolver : IMazeSolver
    {
        private List<Position> path;
        private Position start;
        private Position end;
        private List<List<bool>> visited;
        private Maze mazeModel;
        private int SizeX;
        private int SizeY;

        public MazePath solve(Maze mazeModel, Position start, Position end)
        {
            path = new List<Position>();
            this.start = start;
            this.end = end;
            this.mazeModel = mazeModel;
            SizeX = mazeModel.maze.Count();
            SizeY = mazeModel.maze[0].Count();
            visited = Enumerable.Range(0, SizeX).Select(_ => Enumerable.Repeat(false, SizeY).ToList()).ToList();
            Console.WriteLine("BFS");
            bool isPathFound = BfsRecursion(Direction.NONE, start);
            if(isPathFound){
                path.Add(end);
            }
            Console.WriteLine(this.path.Count);
            return new MazePath() { status = isPathFound? PathFindingStatus.FOUND: PathFindingStatus.NOT_FOUND, path = this.path };
        }

        private bool BfsRecursion(Direction previousDir, Position current)
        {
            if (current.equals(end))
            {
                return true;
            }

            setVisited(current);

            path.Add(current);
            //going down
            if (previousDir != Direction.UP)
            {
                if (current.isDirectionValid(Direction.DOWN, SizeX, SizeY))
                {
                    if (!this.mazeModel.maze[current.x][current.y].downWall)
                    {
                        var nextPosition = current.getPositionInDirection(Direction.DOWN);
                        if (!isVisited(nextPosition))//idziemy w gore
                        {
                            if(BfsRecursion(Direction.DOWN, nextPosition))
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            //going right
            if (previousDir != Direction.LEFT)
            {
                if (current.isDirectionValid(Direction.RIGHT, SizeX, SizeY))
                {
                    if (!this.mazeModel.maze[current.x][current.y].rightWall)
                    {
                        var nextPosition = current.getPositionInDirection(Direction.RIGHT);
                        if (!isVisited(nextPosition))//idziemy w gore
                        {
                            if(BfsRecursion(Direction.RIGHT, nextPosition))
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            if (previousDir != Direction.DOWN)
            {
                if (current.isDirectionValid(Direction.UP, SizeX, SizeY))
                {
                    if (!this.mazeModel.maze[current.x][current.y].upWall)
                    {
                        var nextPosition = current.getPositionInDirection(Direction.UP);
                        if (!isVisited(nextPosition))//idziemy w gore
                        {
                            if(BfsRecursion(Direction.UP, nextPosition))
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            if (previousDir != Direction.RIGHT)
            {
                if (current.isDirectionValid(Direction.LEFT, SizeX, SizeY))
                {
                    if (!this.mazeModel.maze[current.x][current.y].leftWall)
                    {
                        var nextPosition = current.getPositionInDirection(Direction.LEFT);
                        if (!isVisited(nextPosition))//idziemy w gore
                        {
                            if(BfsRecursion(Direction.LEFT, nextPosition))
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            path.RemoveAt(path.Count -1 );
            return false;
        }

        private void setVisited(Position pos)
        {
            visited[pos.x][pos.y] = true;
        }

        private bool isVisited(Position pos)
        {
            return visited[pos.x][pos.y];
        }

        // private bool checkWalls(Direction direction)
        // {
        //     return direction switch
        //     {
        //         Direction.UP =>
        //     }
        // }
    }
}