using System.ComponentModel.DataAnnotations;

namespace MazeService.Models
{
    public class Maze
    {
        [Key]
        [Required]
        public long Id { get; set; }
        [Required]
        public long UserId { get; set; }
        [Required] 
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}