using Domain.Enums;

namespace Domain.Entities
{
    public class Log
    {
        public string LogId { get; set; }
        public LogTypes Type { get; set; } 
        public string Message { get; set; }
        public DateTime Date { get; set; }
    }
}
