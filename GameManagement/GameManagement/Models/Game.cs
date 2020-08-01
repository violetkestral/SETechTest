using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameManagement.Models
{
    public class Game
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public HashSet<Platform> Platforms { get; set; }
    }
}
