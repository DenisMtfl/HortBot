using System.Text.Json.Serialization;

namespace HortBot.Model.Kids
{
    public class Allow
    {
        [JsonPropertyName("presences")]
        public bool Presences { get; set; }

        [JsonPropertyName("events")]
        public bool Events { get; set; }
    }

    public class AllowData
    {
        [JsonPropertyName("PRESENCES")]
        public bool PRESENCES { get; set; }

        [JsonPropertyName("EVENTS")]
        public bool EVENTS { get; set; }
    }

    public class Data
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("institution")]
        public Institution Institution { get; set; }

        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string LastName { get; set; }

        [JsonPropertyName("picture")]
        public string Picture { get; set; }

        [JsonPropertyName("kid_group")]
        public string KidGroup { get; set; }

        [JsonPropertyName("allow")]
        public Allow Allow { get; set; }
    }

    public class EnableMessageType
    {
        [JsonPropertyName("ABSENT_SICK")]
        public bool ABSENTSICK { get; set; }

        [JsonPropertyName("ABSENT_HOLIDAY")]
        public bool ABSENTHOLIDAY { get; set; }

        [JsonPropertyName("ABSENT_OTHER")]
        public bool ABSENTOTHER { get; set; }

        [JsonPropertyName("DEFAULT")]
        public bool DEFAULT { get; set; }

        [JsonPropertyName("PICKUP")]
        public bool PICKUP { get; set; }

        [JsonPropertyName("WALKALONE")]
        public bool WALKALONE { get; set; }
    }

    public class Institution
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("zipcode")]
        public string Zipcode { get; set; }

        [JsonPropertyName("street")]
        public string Street { get; set; }

        [JsonPropertyName("phone")]
        public string Phone { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("settings")]
        public Settings Settings { get; set; }

        [JsonPropertyName("created")]
        public DateTime Created { get; set; }

        [JsonPropertyName("updated")]
        public DateTime Updated { get; set; }
    }

    public class Kids
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("data")]
        public List<Data> Data { get; set; }
    }

    public class Settings
    {
        [JsonPropertyName("enableMessageType")]
        public EnableMessageType EnableMessageType { get; set; }

        [JsonPropertyName("allowData")]
        public AllowData AllowData { get; set; }
    }
}