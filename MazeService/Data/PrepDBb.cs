using MazeService.Models;
using Microsoft.EntityFrameworkCore;

namespace MazeService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app, bool isProd)
        {
            using(var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProd);
            }
        }

        private static void SeedData(AppDbContext context, bool isProd)
        {
            if(isProd)
            {
                Console.WriteLine("Attempting migration");
                try
                {
                    context.Database.Migrate();
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Migration failed {ex.Message}");
                }
               
            }

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