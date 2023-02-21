using Microsoft.EntityFrameworkCore;
using MazeService.Models;

namespace MazeService.Data 
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {
            
        }

        public DbSet<Maze> Mazes { get; set; }
    }
}