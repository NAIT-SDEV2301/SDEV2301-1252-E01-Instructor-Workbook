using Lesson37_MauiBlazorNavDiExercise.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson37_MauiBlazorNavDiExercise.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetProducts();
        Product? GetProduct(int id);
    }
}
