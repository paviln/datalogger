using System.Text.Json.Serialization;

namespace App.Models
{
    public class Plant
    {
        [JsonPropertyName("_id")]
        public string Id { get; set; }
        public string Name { get; set; }
        public string LoggerId { get; set; }
        public string MinimumTemperature { get; set; }
        public SoilType SoilType { get; set; }
        public Image Img { get; set; }
        public Status Status { get; set; }
        public Log[] Logs { get; set; }
    }
}
