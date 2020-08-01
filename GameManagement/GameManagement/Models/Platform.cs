using System.ComponentModel.DataAnnotations;

namespace GameManagement.Models
{
    public class Platform
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
