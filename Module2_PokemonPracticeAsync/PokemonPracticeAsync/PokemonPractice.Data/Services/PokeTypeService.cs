using Microsoft.EntityFrameworkCore;
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
    
        public async Task<PokeType> AddTypeAsync(string typeName)
        {
            // Reject blank names
            ArgumentNullException.ThrowIfNullOrWhiteSpace(typeName, nameof(typeName));

            // Reject duplicate names (case-insensitive)
            if (await _db.PokeTypes.AnyAsync(t => t.Name.ToLower() == typeName.Trim().ToLower()))
            {
                throw new InvalidOperationException($"A Type with the name '{typeName}' already exists.");
            }

            // Adds a Pokémon type
            var poketype = new PokeType
            {
                Name = typeName.Trim()
            };
            await _db.PokeTypes.AddAsync(poketype);
            await _db.SaveChangesAsync();
            return poketype;
        }

        public async Task<PokeType?> GetPokeTypeByNameAsync(string typeName)
        {
            // Reject blank names
            ArgumentNullException.ThrowIfNullOrWhiteSpace(typeName, nameof(typeName));
            // Returns a Type by name, or null if not found
            return await _db.PokeTypes
                .SingleOrDefaultAsync(t => t.Name == typeName.Trim());
        }

        public async Task<PokeType?> GetPokeTypeByIdAsync(int typeId)
        {
            // Returns a Type by id, or null if not found
            return await _db.PokeTypes
                .SingleOrDefaultAsync(t => t.Id == typeId)
                ?? throw new KeyNotFoundException($"PokeType with ID {typeId} was not found.");
        }   
        public async Task<List<PokeType>> ListPokeTypesAsync()
        {
            return await _db.PokeTypes
                .OrderBy(p => p.Name)
                .ToListAsync();
        }
    }
}
