using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PokemonPractice.Data.Models
{
    public class Pokemon
    {
        // Primary key for the Pokemon entity.
        public int Id { get; set; }
        // Using `required` to enforce initialization instead of defaulting to string.Empty.
        // Prevents accidental creation of objects with empty required fields.
        //public required string Name { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;

        // Navigation property to represent the many-to-many relationship with PokeType.
        public List<PokeType> Types { get; set; } = new();

        override public string ToString()
        {
            return $"Pokemon {{ Id = {Id}, Name = {Name} }}";
        }

    }
}
