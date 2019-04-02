namespace UFirstTechTaskByLu.Models
{
    public class ParsedLog
    {
        public string Host { get; set; }
        public Date DateTime { get; set; }
        public Request Request { get; set; }
        public string ResponseCode { get; set; }
        public string Size { get; set; }
    }
}
