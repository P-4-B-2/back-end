using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace backend.DAL.Models
{
    public class Conversation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime StartDatetime { get; set; }
        public DateTime? EndDatetime { get; set; }
        public int? Sentiment { get; set; }
        public string? Summary { get; set; }

        public int BenchId { get; set; }
        [JsonIgnore]
        public Bench? Bench{ get; set; }

        [JsonIgnore]
        public ICollection<Answer>? Answers { get; set; }
    }
}
