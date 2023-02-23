namespace HortBot.Model
{
    public class PresencesPerUser
    {
        public long ChatId { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public bool? StartMsgSent { get; set; }
        public bool? EndMsgSent { get; set; }
    }
}