using System.ComponentModel.DataAnnotations;

namespace GameManagement.Models
{
    public class Game
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public Platform[] Platforms { get; set; }
    }
}
