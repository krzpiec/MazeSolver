using MazeService.Models;

namespace MazeService.Data
{
    public class MazeRepo : IMazeRepo
    {
        private readonly AppDbContext _context;

        public MazeRepo(AppDbContext context)
        {
            _context = context;
        }

        public long CreateMaze(Maze maze)
        {
            if (maze == null)
            {
                throw new ArgumentNullException(nameof(maze));
            }

            var createdEntity = _context.Mazes.Add(maze);
            return createdEntity.Entity.Id;
        }

        public IEnumerable<Maze> GetAllMazes()
        {
            return _context.Mazes.ToList();
        }

        public Maze GetMazeById(long id)
        {
            return _context.Mazes.FirstOrDefault(p => p.Id == id);
        }

        public bool saveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}