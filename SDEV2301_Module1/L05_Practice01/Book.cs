using System;
using System.Collections.Generic;
using System.Text;

namespace L05_Practice01
{
    public class Book
    {
        public string Title;
        public string Author;
        public int Pages;

        public void PrintSummary()
        {
            Console.WriteLine($"Title: {Title}");
            Console.WriteLine($"Author: {Author}");
            Console.WriteLine($"Pages: {Pages}");
            Console.WriteLine();
        }
    }
}
