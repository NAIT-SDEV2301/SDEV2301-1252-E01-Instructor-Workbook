// Required packages:
//   dotnet add package Microsoft.EntityFrameworkCore
//   dotnet add package Microsoft.EntityFrameworkCore.Sqlite
using Microsoft.EntityFrameworkCore;

namespace Module02.Lesson17.EfCoreMigration
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        // Foreign key
        public int CategoryId { get; set; }

        // Navigation property
        public Category Category { get; set; }
    }

    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }

        // One category --> many products
        public List<Product> Products { get; set; } = new();
    }

    public class AppDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

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
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
}
