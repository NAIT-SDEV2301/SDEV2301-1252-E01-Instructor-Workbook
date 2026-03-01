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

      
        public void SeedIfEmpty()
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
            if (_db.Pokemons.Any())
                return;

            var pikachu = _pokemonService.AddPokemon("Pikachu");
            var gyarados = _pokemonService.AddPokemon("Gyarados");
            var bulbasaur = _pokemonService.AddPokemon("Bulbasaur");

            var electric = _typeService.AddType("Electric");
            var water = _typeService.AddType("Water");
            var flying = _typeService.AddType("Flying");
            var grass = _typeService.AddType("Grass");
            var poison = _typeService.AddType("Poison");

            _linkService.LinkTypeToPokemon(pikachu.Id, electric.Id);
            _linkService.LinkTypeToPokemon(gyarados.Id, water.Id);
            _linkService.LinkTypeToPokemon(gyarados.Id, flying.Id);
            _linkService.LinkTypeToPokemon(bulbasaur.Id, grass.Id);
            _linkService.LinkTypeToPokemon(bulbasaur.Id, poison.Id);
        }
    }
}
