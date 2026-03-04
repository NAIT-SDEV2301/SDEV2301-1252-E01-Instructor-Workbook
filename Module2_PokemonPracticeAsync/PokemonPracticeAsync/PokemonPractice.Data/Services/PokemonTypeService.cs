using Microsoft.EntityFrameworkCore;    // Required for Include() method to load related data
using PokemonPractice.Data.Data;
using PokemonPractice.Data.Models;

namespace PokemonPractice.Data.Services
{
    public class PokemonTypeService
    {
        private readonly AppDbContext _db;

        public PokemonTypeService(AppDbContext db)
        {
            _db = db;
        }

        public async Task LinkTypeToPokemonAsync(int pokemonId, int typeId)
        {
            // In this course, we use Single() instead of Find() because it works with Include() and makes our intent explicit
            var pokemon = await _db.Pokemons
                .Include(p => p.Types)
                .SingleOrDefaultAsync(p => p.Id == pokemonId)
                ?? throw new KeyNotFoundException($"Pokemon with ID {pokemonId} was not found.");

            var poketype = await _db.PokeTypes
                .SingleOrDefaultAsync(t => t.Id == typeId)
                ?? throw new KeyNotFoundException($"PokeType with ID {typeId} was not found.");

            // Must prevent duplicate links (cannot link the same Type twice)
            if (pokemon.Types.Any(t => t.Id == typeId))
            {
                throw new InvalidOperationException($"This Pokemon is already linked to that Type.");
            }

            // Links a Pokémon to a Type
            pokemon.Types.Add(poketype);
            await _db.SaveChangesAsync();
        }

        public async Task UnlinkTypeFromPokemonAsync(int pokemonId, int typeId)
        {
            // In this course, we use Single() instead of Find() because it works with Include() and makes our intent explicit
            var pokemon = _db.Pokemons
                .Include(p => p.Types)
                .SingleOrDefault(p => p.Id == pokemonId)
                ?? throw new KeyNotFoundException($"Pokemon with ID {pokemonId} was not found.");

            var poketype = _db.PokeTypes
                .SingleOrDefault(t => t.Id == typeId)
                ?? throw new KeyNotFoundException($"PokeType with ID {typeId} was not found.");

            // Removes the relationship only
            // Does not delete the Pokémon or the Type
            pokemon.Types.Remove(poketype);
            await _db.SaveChangesAsync();
        }

    }
}
