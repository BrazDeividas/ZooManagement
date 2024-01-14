namespace API.Models
{
    public class Animal
    {
        public int Id { get; set; }
        public string Species { get; set; }
        public string Food { get; set; }
        public Enclosure Enclosure { get; set; }
        public DateTime ImportedDate { get; set; }
    }
}