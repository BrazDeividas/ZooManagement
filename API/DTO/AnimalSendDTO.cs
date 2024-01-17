using System.Text.Json.Serialization;

namespace API.DTO
{
    public class AnimalSendDTO
    {
        public string? Species { get; set; }
        public string? Food { get; set; }
        [JsonIgnore]
        public string? Enclosure { get; set; }
    }
}