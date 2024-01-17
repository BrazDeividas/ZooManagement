using System.Text.Json.Serialization;

namespace API.DTO
{
    public class EnclosureSendDTO
    {
        public string? Name { get; set; }
        public string? Size { get; set; }
        [JsonIgnore]
        public int MaxCapacity { get; set; }
        [JsonIgnore]
        public int CurrentCapacity { get; set; }
        public string? Location { get; set; }
        [JsonIgnore]
        public List<string>? Objects { get; set; }
        [JsonIgnore]
        public List<string>? Species { get; set; }
    }
}