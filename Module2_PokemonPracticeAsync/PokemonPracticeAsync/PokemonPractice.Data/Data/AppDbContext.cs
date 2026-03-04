using Microsoft.EntityFrameworkCore;
using PokemonPractice.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonPractice.Data.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        // DbSet properties for each entity in the model.
        // EF Core initializes DbSet properties at runtime.
        // = default!; suppresses nullable warnings.
        public DbSet<Pokemon> Pokemons { get; set; } = default!;
        public DbSet<PokeType> PokeTypes { get; set; } = default!;

        // Configuring the many-to-many relationship between Pokemon and PokeType.
        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // EF Core automatically:
            //  1) creates a join table
            //  2) Names it automatically (e.g., PokemonPokeType)
            //  3) Creates foreign keys automatically
            // No explicit join entity needed
            modelBuilder.Entity<Pokemon>()
                .HasMany(p => p.Types)
                .WithMany(t => t.Pokemons);
        }
    }
}
