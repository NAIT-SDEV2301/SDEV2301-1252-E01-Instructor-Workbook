using Microsoft.EntityFrameworkCore;    // Required for Include() method to load related data
using PokemonPractice.Data.Data;
using PokemonPractice.Data.Models;

namespace PokemonPractice.Data.Services
{
    public class DatabaseSeeder
    {
        private readonly AppDbContext _db;
        private readonly PokemonService _pokemonService;
        private readonly PokeTypeService _typeService;
        private readonly PokemonTypeService _linkService;

        public DatabaseSeeder(
            AppDbContext db,
            PokemonService pokemonService,
            PokeTypeService typeService,
            PokemonTypeService linkService)
        {
            _db = db;
            _pokemonService = pokemonService;
            _typeService = typeService;
            _linkService = linkService;
        }

      
        public async Task SeedIfEmptyAsync()
        {
            /*
             * If the database is empty on first run, you may add a few defaults so you can demo quickly:
             *      Pokémon: Pikachu, Gyarados, Bulbasaur
             *      Types: Electric, Water, Flying, Grass, Poison
             *      Links:
             *          Pikachu → Electric
             *          Gyarados → Water, Flying
             *          Bulbasaur → Grass, Poison;
            */
            if (await _db.Pokemons.AnyAsync())
                return;

            var pikachu = await _pokemonService.AddPokemonAsync("Pikachu");
            var gyarados = await _pokemonService.AddPokemonAsync("Gyarados");
            var bulbasaur = await _pokemonService.AddPokemonAsync("Bulbasaur");

            var electric = await _typeService.AddTypeAsync("Electric");
            var water = await _typeService.AddTypeAsync("Water");
            var flying = await _typeService.AddTypeAsync("Flying");
            var grass = await _typeService.AddTypeAsync("Grass");
            var poison = await _typeService.AddTypeAsync("Poison");

            await _linkService.LinkTypeToPokemonAsync(pikachu.Id, electric.Id);
            await _linkService.LinkTypeToPokemonAsync(gyarados.Id, water.Id);
            await _linkService.LinkTypeToPokemonAsync(gyarados.Id, flying.Id);
            await _linkService.LinkTypeToPokemonAsync(bulbasaur.Id, grass.Id);
            await _linkService.LinkTypeToPokemonAsync(bulbasaur.Id, poison.Id);
        }
    }
}
