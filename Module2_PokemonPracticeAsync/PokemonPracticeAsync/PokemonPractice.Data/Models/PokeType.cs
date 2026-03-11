using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PokemonPractice.Data.Models
{
    public class PokeType
    {
        // Primary key for the PokeType entity.
        public int Id { get; set; }
        // Using `required` to enforce initialization instead of defaulting to string.Empty.
        // Prevents accidental creation of objects with empty required fields.
        //public required string Name { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        // Navigation property to represent the many-to-many relationship with Pokemon.
        public List<Pokemon> Pokemons { get; set; } = new();

        override public string ToString()
        {
            return $"PokeType {{ Id = {Id}, Name = {Name} }}";
        }
    }
}
