using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Animal
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Species { get; set; }
        [Required]
        public string? Food { get; set; }
        public Enclosure? Enclosure { get; set; }
        public int EnclosureId { get; set; }
        [Required]
        public DateTime ImportedDate { get; set; } = DateTime.Now;
    }
}