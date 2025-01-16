namespace backend.DAL.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }

        public ICollection<History> Histories { get; set; }
    }
}
