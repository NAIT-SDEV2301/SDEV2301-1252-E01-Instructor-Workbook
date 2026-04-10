using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson37_MauiBlazorNavDiExercise.Services
{
    public class SimpleDiscountService : IDiscountService
    {
        private const decimal DiscountRate = 0.10m;// 10% off
        decimal IDiscountService.ApplyDiscount(decimal originalPrice)
        {
            // Return the discounted price
            return originalPrice * (1 - DiscountRate);
        }
    }
}
