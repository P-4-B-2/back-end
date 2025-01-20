namespace backend.DTOs
{
    public class AnswerDTO
    {
        public int Id { get; set; }
        public string Response { get; set; }
        public int ConversationId { get; set; }
        public int QuestionId { get; set; }
    }
}
