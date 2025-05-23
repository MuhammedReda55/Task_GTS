﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Task_GTS.Models
{

    public class Category
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Minimum length must be 3")]
        [MaxLength(50, ErrorMessage = "Maximum length must be 50")]
        public string Name { get; set; }
        [JsonIgnore]
        public ICollection<Product> Products { get; } = new List<Product>();
    }
}
