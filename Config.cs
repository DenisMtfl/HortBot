namespace HortBot
{
    public class Config
    {
        public string TelegramBotToken { get; set; }
        public HortProLogin HortProLogin { get; set; }
    }

    public class HortProLogin
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
