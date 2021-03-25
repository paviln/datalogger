using System.Text.Json.Serialization;

namespace App.Models
{
    public class Logger
    {
        [JsonPropertyName("_id")]
        public string Id { get; set; }
        public Log Logs { get; set; }
        public Plant Plants { get; set; }
    }
}
