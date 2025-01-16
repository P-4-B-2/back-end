using Microsoft.CodeAnalysis;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace backend.DAL.Models
{
    public class History
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int BenchId { get; set; }
        public Bench Bench { get; set; }

        public int LocationId { get; set; }
        public Location Location { get; set; }

        public int StatusId { get; set; }
        public Status Status { get; set; }


    }
}
