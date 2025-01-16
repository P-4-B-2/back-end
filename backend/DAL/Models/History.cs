using Microsoft.CodeAnalysis;

namespace backend.DAL.Models
{
    public class History
    {
        public int Id { get; set; }

        public int BenchId { get; set; }
        public Bench Bench { get; set; }

        public int LocationId { get; set; }
        public Location Location { get; set; }

        public int StatusId { get; set; }
        public Status Status { get; set; }


    }
}
