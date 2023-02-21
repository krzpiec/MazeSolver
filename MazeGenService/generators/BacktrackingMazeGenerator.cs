using System.Collections;
using MazeGenService.Models;

namespace MazeGenService.Generators
{
    public class BacktrancingMazeGenerator : IMazeGenerator
    {
        private readonly int sizeX;
        private readonly int sizeY;
        private int visitedCount = 0;
        private List<List<bool>> visited;
        private List<List<Cell>> maze;
        public BacktrancingMazeGenerator(int SizeX, int SizeY)
        {
            sizeX = SizeX;
            sizeY = SizeY;
            visited = Enumerable.Range(0, SizeX).Select(_ => Enumerable.Repeat(false, SizeY).ToList()).ToList();
            maze = Maze.initialMaze(SizeX, sizeY).maze;      
        }

        public List<List<Cell>> generateMaze()
        {
            Stack<Cell> stack = new Stack<Cell>();
            Cell current = maze[0][0];
            stack.Push(current);
            markVisited(current);
            while (stack.Count != 0)
            {
                current = stack.Pop();
                Cell? next = getRandomUnvisitedNeighbour(current);
                if (next != null)
                {
                    stack.Push(current);
                    removeWalls(current, next);
                    markVisited(next);
                    stack.Push(next);
                }
            }
            
            return maze;
        }

        //top right bottom left
        private Cell? getRandomUnvisitedNeighbour(Cell current)
        {
            List<Cell> neighbours = new List<Cell>();
            if (current.posX > 0)
            {
                if (!isVisited(maze[current.posX - 1][current.posY]))
                {
                    neighbours.Add(maze[current.posX - 1][current.posY]);
                }

            }
            if (current.posY < sizeY - 1)
            {
                if (!isVisited(maze[current.posX ][current.posY + 1]))
                {
                    neighbours.Add(maze[current.posX][current.posY + 1]);
                }

            }
            if (current.posX < sizeX - 1)
            {
                if (!isVisited(maze[current.posX + 1][current.posY]))
                {
                    neighbours.Add(maze[current.posX + 1][current.posY]);
                }

            }
            if (current.posY > 0)
            {
                if (!isVisited(maze[current.posX][current.posY - 1]))
                {
                    neighbours.Add(maze[current.posX][current.posY - 1]);
                }

            }
            
            if (neighbours.Count == 0)
            {
                return null;
            }

            var random = new Random();
            int index = random.Next(neighbours.Count);
            return neighbours[index];
        }

        private bool isVisited(Cell current)
        {
            return visited[current.posX][current.posY];
        }

        private void markVisited(Cell current)
        {
            visited[current.posX][current.posY] = true;
            visitedCount++;
        }

        private void removeWalls(Cell a, Cell b)
        {
            int diffX = a.posX - b.posX;
            if (diffX == 1)
            {
                a.upWall = false;
                b.downWall = false;
                
            }
            else if (diffX == -1) 
            {
                a.downWall = false;
                b.upWall = false;
            }

            int diffY = a.posY - b.posY;
            if (diffY == 1)
            {
                a.leftWall = false;
                b.rightWall = false;
            } //a
              //b
            else if (diffY == -1)
            {
                a.rightWall = false;
                b.leftWall = false;
                
            }

            maze[a.posX][a.posY] = a;
            maze[b.posX][b.posY] = b;
        }
    }
}