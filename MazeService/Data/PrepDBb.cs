using MazeService.Models;
using Microsoft.EntityFrameworkCore;

namespace MazeService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using(var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }

        private static void SeedData(AppDbContext context)
        {
            if(!context.Mazes.Any())
            {
                Console.WriteLine("Seeding");
                context.Mazes.AddRange(
                    new Maze() {Name = "Testing1", UserId = 0, Description = "desc1"},
                    new Maze() {Name = "Testing2", UserId = 0, Description = "desc2"},
                    new Maze() {Name = "Testing3", UserId = 0, Description = "desc3"}
                );
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("We already have data");
            }
        }
    }
}