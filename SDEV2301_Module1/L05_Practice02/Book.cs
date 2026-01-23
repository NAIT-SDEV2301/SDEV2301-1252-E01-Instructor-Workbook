using System;
using System.Collections.Generic;
using System.Text;

namespace L05_Practice02
{
    public class Book
    {
        public string Title { get; }
        public string Author { get; }
        public int Pages { get; }

        public Book(string title, string author, int pages)
        {
            Title = title;
            Author = author;
            Pages = pages;
        }

        public void PrintSummary()
        {
            Console.WriteLine($"{Title} by {Author}, {Pages} pages");
        }
    }
}
