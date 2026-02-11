using System;
using System.Collections.Generic;
using System.Text;

namespace Module02.Lesson16.EfCoreIntro
{
    public class Product
    {
        public int Id { get; set; } // Primary key convention
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
