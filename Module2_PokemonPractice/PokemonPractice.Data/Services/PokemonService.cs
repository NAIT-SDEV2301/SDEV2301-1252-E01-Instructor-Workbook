using Microsoft.EntityFrameworkCore;    // Required for Include() method to load related data
using PokemonPractice.Data.Data;
using PokemonPractice.Data.Models;

namespace PokemonPractice.Data.Services
{
    public class PokemonService
    {
        private readonly AppDbContext _db;

        public PokemonService(AppDbContext db)
        {
            _db = db;
        }

        public Pokemon AddPokemon(string pokemonName)
        {
            // Reject blank names
            ArgumentException.ThrowIfNullOrWhiteSpace(pokemonName, nameof(pokemonName));

            // Reject duplicate names (case-insensitive)
            if (_db.Pokemons.Any(p => p.Name.ToLower() == pokemonName.Trim().ToLower()))
            {
                throw new InvalidOperationException($"A Pokémon with the name '{pokemonName}' already exists.");
            }

            // Adds a Pokémon
            var pokemon = new Pokemon
            {
                Name = pokemonName.Trim()
            };
            _db.Pokemons.Add(pokemon);
            _db.SaveChanges();
            return pokemon;
        }

        public Pokemon GetPokemonById(int pokemonId)    
        {
            // Returns a Pokémon by id, or null if not found
            return _db.Pokemons
                .SingleOrDefault(p => p.Id == pokemonId)
                ?? throw new KeyNotFoundException($"Pokemon with ID {pokemonId} was not found.");
        }

        public List<Pokemon> ListPokemonWithTypes()
        {
            // Returns data that can be printed like:
            //      Pikachu: Electric
            //      Gyarados: Water, Flying
            // Must use Include() to load related data
            return _db.Pokemons
                .Where(p => p.Types.Count > 0)   // Only include Pokémon that have at least one Type
                .Include(p => p.Types)
                .ToList();
        }

        public List<Pokemon> ListPokemons()
        {
            return _db.Pokemons
                .OrderBy(p => p.Name)
                .ToList();
        }

    }
}
