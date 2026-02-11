using Microsoft.EntityFrameworkCore; // for DbContext

namespace Module02.Lesson16.EfCoreIntro
{
    public class AppDbContext : DbContext // DbSession
    {
        public DbSet<Product> Products => Set<Product>(); // table abstraction

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // Force SQLite to use a single database file in the project root.
            // Prevents "no such table" errors caused by different working
            // directories between `dotnet run` and Visual Studio.
            var dbPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "app.db");
            dbPath = Path.GetFullPath(dbPath);

            options.UseSqlite($"Data Source={dbPath}");
        }
    }
}
