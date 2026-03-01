using PokemonPractice.Data.Data;
using PokemonPractice.Data.Models;

namespace PokemonPractice.Data.Services
{
    public class PokeTypeService
    {
        private readonly AppDbContext _db;

        public PokeTypeService(AppDbContext db)
        {
            _db = db;
        }
    
        public PokeType AddType(string typeName)
        {
            // Reject blank names
            ArgumentNullException.ThrowIfNullOrWhiteSpace(typeName, nameof(typeName));

            // Reject duplicate names (case-insensitive)
            if (_db.PokeTypes.Any(t => t.Name.ToLower() == typeName.Trim().ToLower()))
            {
                throw new InvalidOperationException($"A Type with the name '{typeName}' already exists.");
            }

            // Adds a Pokémon type
            var poketype = new PokeType
            {
                Name = typeName.Trim()
            };
            _db.PokeTypes.Add(poketype);
            _db.SaveChanges();
            return poketype;
        }

        public PokeType? GetPokeTypeByName(string typeName)
        {
            // Reject blank names
            ArgumentNullException.ThrowIfNullOrWhiteSpace(typeName, nameof(typeName));
            // Returns a Type by name, or null if not found
            return _db.PokeTypes
                .SingleOrDefault(t => t.Name == typeName.Trim());
        }

        public PokeType? GetPokeTypeById(int typeId)
        {
            // Returns a Type by id, or null if not found
            return _db.PokeTypes
                .SingleOrDefault(t => t.Id == typeId)
                ?? throw new KeyNotFoundException($"PokeType with ID {typeId} was not found.");
        }   
        public List<PokeType> ListPokeTypes()
        {
            return _db.PokeTypes
                .OrderBy(p => p.Name)
                .ToList();
        }
    }
}
