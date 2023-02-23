
using System.Text.Json.Serialization;

namespace HortBot.Model
{
    public class Login
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

        public Login(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}