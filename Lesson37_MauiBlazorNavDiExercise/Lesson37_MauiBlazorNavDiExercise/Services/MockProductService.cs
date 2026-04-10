using Lesson37_MauiBlazorNavDiExercise.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson37_MauiBlazorNavDiExercise.Services
{
    public class MockProductService : IProductService
    {
        private readonly List<Product> _products = new()
        {
            new Product { Id = 1, Name = "Wireless Mouse", Category = "Accessories", Price = 24.99m, Description = "Comfortable wireless mouse with long battery life." },
            new Product { Id = 2, Name = "Mechanical Keyboard", Category = "Accessories", Price = 89.99m, Description = "Tactile switches with RGB backlight." },
            new Product { Id = 3, Name = "27\" Monitor", Category = "Displays", Price = 279.00m, Description = "QHD display with thin bezels." },
            new Product { Id = 4, Name = "USB-C Hub", Category = "Accessories", Price = 49.99m, Description = "Multi-port USB-C hub with HDMI and Ethernet." }
        };
        public Product? GetProduct(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Product> GetProducts()
        {
            return _products;
        }
    }
}
