namespace backend.DTOs
{
    public class QuestionDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime MadeAt { get; set; }
        public bool IsActive { get; set; }
        public int OrderNumber { get; set; }
    }
}
