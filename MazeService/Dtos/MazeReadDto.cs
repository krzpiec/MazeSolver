using System.ComponentModel.DataAnnotations;

namespace MazeService.Dtos
{
    public class MazeReadDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}