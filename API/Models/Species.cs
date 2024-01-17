using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Species
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public int EnclosureId { get; set; }
        [Required]
        public Enclosure? Enclosure { get; set; }
    }
}