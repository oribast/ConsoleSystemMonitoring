using System.Text.Json.Serialization;

namespace ConsoleSystemMonitoring
{
    internal class Configuration
    {
        public int CollectionInterval { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public OutputFormat OutputFormat { get; set; }
        public bool UseTimestamp { get; set; }
        public string? LogFileName { get; set; }
    }
}
