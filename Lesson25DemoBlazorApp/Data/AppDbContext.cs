using Lesson25DemoBlazorApp.Models;
using Microsoft.EntityFrameworkCore;

namespace Lesson25DemoBlazorApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { }

        public DbSet<Product> Products { get; set; }
    }

}
