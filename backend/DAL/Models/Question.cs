﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace backend.DAL.Models
{
    public class Question
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime made_at { get; set; }
        public bool is_active { get; set; } 
        public int order_number { get; set; }

        [JsonIgnore]
        public ICollection<Answer> Answers { get; set; } = new List<Answer>();

    }
}