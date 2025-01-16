namespace backend.DAL.Models
{
    public class Bench
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Conversation> Conversations { get; set; }
    }
}
