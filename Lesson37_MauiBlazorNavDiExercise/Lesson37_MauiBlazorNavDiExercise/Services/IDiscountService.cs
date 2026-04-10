using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson37_MauiBlazorNavDiExercise.Services
{
    public interface IDiscountService
    {
        decimal ApplyDiscount(decimal originalPrice);
    }
}
