using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace backend.DAL.Models
{
    public class Answer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Response { get; set; }

        [Required]
        public int ConversationId { get; set; }

        [JsonIgnore]
        public Conversation? Conversation { get; set; }

        [Required]
        public int QuestionId { get; set; }

        [JsonIgnore]
        public Question? Question { get; set; }

        public List<string>? Keywords { get; set; }


    }
}
