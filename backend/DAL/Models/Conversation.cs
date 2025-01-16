namespace backend.DAL.Models
{
    public class Conversation
    {
        public int Id { get; set; }
        public DateTime start_datetime { get; set; }
        public DateTime end_datetime { get; set; }
        public int sentiment { get; set; }
        public string summary { get; set; }

        public int BenchId { get; set; }
        public Bench Bench{ get; set; }

        public ICollection<Answer> Answers { get; set; }
    }
}
