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

        public async Task<Pokemon> AddPokemonAsync(string pokemonName)
        {
            // Reject blank names
            ArgumentException.ThrowIfNullOrWhiteSpace(pokemonName, nameof(pokemonName));

            // Reject duplicate names (case-insensitive)
            if (await _db.Pokemons.AnyAsync(p => p.Name.ToLower() == pokemonName.Trim().ToLower()))
            {
                throw new InvalidOperationException($"A Pokémon with the name '{pokemonName}' already exists.");
            }

            // Adds a Pokémon
            var pokemon = new Pokemon
            {
                Name = pokemonName.Trim()
            };
            await _db.Pokemons.AddAsync(pokemon);
            await _db.SaveChangesAsync();
            return pokemon;
        }

        public async Task<Pokemon?> GetPokemonByIdAsync(int pokemonId)    
        {
            // Returns a Pokémon by id, or null if not found
            return await _db.Pokemons
                .SingleOrDefaultAsync(p => p.Id == pokemonId)
                ?? throw new KeyNotFoundException($"Pokemon with ID {pokemonId} was not found.");
        }

        public async Task<List<Pokemon>> ListPokemonWithTypesAsync()
        {
            // Returns data that can be printed like:
            //      Pikachu: Electric
            //      Gyarados: Water, Flying
            // Must use Include() to load related data
            return await _db.Pokemons
                .Where(p => p.Types.Count > 0)   // Only include Pokémon that have at least one Type
                .Include(p => p.Types)
                .ToListAsync();
        }

        public async Task<List<Pokemon>> ListPokemonsAsync()
        {
            return await _db.Pokemons
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

    }
}
