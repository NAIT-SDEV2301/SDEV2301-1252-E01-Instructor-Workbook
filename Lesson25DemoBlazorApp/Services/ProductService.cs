using Lesson25DemoBlazorApp.Data;
using Lesson25DemoBlazorApp.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Lesson25DemoBlazorApp.Services
{
    // Service layer (BLL) responsible for business operations on Product.
    // Uses IDbContextFactory to safely create short-lived DbContext instances per operation.
    public class ProductService
    {
        // IDbContextFactory creates a NEW DbContext each time we call CreateDbContext().
        // This is recommended for Blazor apps because DbContext is NOT thread-safe
        // and Blazor components can run multiple async operations concurrently.
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;

        public ProductService(IDbContextFactory<AppDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        // "List" is used for returning collections (service naming standard).
        public async Task<List<Product>> ListProductsAsync()
        {
            // using ensures the DbContext is disposed immediately after use.
            // Each method call gets its own short-lived DbContext instance.
            using var context = _dbContextFactory.CreateDbContext();

            return await context.Products.ToListAsync();
        }

        // "Add" returns the created entity so callers receive generated values (e.g., Id).
        public async Task<Product> AddProductAsync(Product product)
        {
            // Service layer validates inputs (defensive programming at boundary).
            ArgumentNullException.ThrowIfNull(product);

            using var context = _dbContextFactory.CreateDbContext();

            context.Products.Add(product);
            await context.SaveChangesAsync();

            return product; // After SaveChanges, EF populates identity values.
        }

        // Update returns the updated entity for UI convenience.
        public async Task<Product> UpdateProductAsync(Product product)
        {
            ArgumentNullException.ThrowIfNull(product);

            using var context = _dbContextFactory.CreateDbContext();

            // For teaching simplicity: directly updating a detached entity.
            // In production: safer pattern is load -> modify -> save.
            context.Products.Update(product);

            await context.SaveChangesAsync();

            return product;
        }

        public async Task DeleteProductAsync(int id)
        {
            using var context = _dbContextFactory.CreateDbContext();

            // FindAsync retrieves by primary key.
            var product = await context.Products.FindAsync(id);

            // Service layer enforces existence rule (fail fast).
            if (product == null)
            {
                throw new KeyNotFoundException($"Product with id {id} was not found.");
            }

            context.Products.Remove(product);
            await context.SaveChangesAsync();
        }
    }

}
