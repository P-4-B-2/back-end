using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace backend.DAL.Models
{
    public class Location
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }

        [JsonIgnore]
        public ICollection<History>? Histories { get; set; }
    }
}
