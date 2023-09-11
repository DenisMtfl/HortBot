using System.Text.Json.Serialization;

namespace HortBot.Model.Presences
{
    public class Data
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("rows")]
        public List<Row> Rows { get; set; } = new List<Row>();
    }

    public class DateEnd
    {
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("timezone_type")]
        public int TimezoneType { get; set; }

        [JsonPropertyName("timezone")]
        public string Timezone { get; set; }
    }

    public class DateStart
    {
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("timezone_type")]
        public int TimezoneType { get; set; }

        [JsonPropertyName("timezone")]
        public string Timezone { get; set; }
    }

    public class Presences
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("data")]
        public Data Data { get; set; }
    }

    public class Row
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("date_start")]
        //public DateStart? DateStart { get; set; }
        public DateTime? DateStart { get; set; }

        [JsonPropertyName("date_end")]
        //public DateEnd? DateEnd { get; set; }
        public DateTime? DateEnd { get; set; }

        [JsonPropertyName("duration")]
        public int? Duration { get; set; }
    }
}