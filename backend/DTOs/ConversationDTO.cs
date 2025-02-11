using backend.DAL.Models;

namespace backend.DTOs
{
    public class ConversationDTO
    {
        public int Id { get; set; }
        public DateTime StartDatetime { get; set; }
        public DateTime? EndDatetime { get; set; }
        public int? Sentiment { get; set; }
        public string? Summary { get; set; }
        public int BenchId { get; set; }
        public ICollection<Answer>? Answers { get; set; }
    }
}
