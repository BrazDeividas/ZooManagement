using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Enclosure
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = "Unnamed";
        [Required]
        public string Size { get; set; }
        public int MaxCapacity { get; set; }
        public int CurrentCapacity { get; set; }
        [Required]
        public string Location { get; set; }
        public List<string> Objects { get; set; } = new List<string>();
        public List<Animal> Animals { get; set; } = new List<Animal>();
        public List<Species> Species { get; set;} = new List<Species>();
    }
}