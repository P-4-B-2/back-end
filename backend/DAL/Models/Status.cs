namespace backend.DAL.Models
{
    public class Status
    {
        public int Id { get; set; }
        public string Type { get; set; }

        public ICollection<History> Histories { get; set; }
    }
}
