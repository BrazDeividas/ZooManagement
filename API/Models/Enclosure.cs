namespace API.Models
{
    public class Enclosure
    {
        public enum EnclosureLocation
        {
            Inside,
            Outside
        }

        public enum EnclosureSize {
            Small,
            Medium,
            Large
        } 

        public int Id { get; set; }
        public string Name { get; set; }
        public EnclosureSize Size { get; set; }
        public int MaxCapacity { get; set; }
        public int CurrentCapacity { get; set; }
        public EnclosureLocation Location { get; set; }
        public List<string> Objects { get; set; }
        public List<Animal> Animals { get; set; }
    }
}