using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson37_MauiBlazorNavDiExercise.Services
{
    public class TimeOfDayDiscountService : IDiscountService
    {
        /// <summary>
        /// Apply a discount from 20% from midnight to 8am
        /// 2% discount from 8am to 8pm,
        /// 15% discount from 8pm - midnight
        /// </summary>
        /// <param name="originalPrice"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public decimal ApplyDiscount(decimal originalPrice)
        {
            int currentHour = DateTime.Now.Hour;
            decimal discountRate = 0.0m;
            if (currentHour >= 0 && currentHour < 8)
            {
                discountRate = 0.20m;
            }
            else if (currentHour >= 8 && currentHour <= 20)
            {
                discountRate = 0.02m;
            }
            else
            {
                discountRate = 0.15m;
            }
            return originalPrice * (1 - discountRate);
        }
    }
}
