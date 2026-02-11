using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson09_UnitTesting.Models
{
    public class Book
    {
        //public string Title { get; private set; }
        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Title is required.", nameof(Title));

                _title = value.Trim();
            }
            
        }
        public int Pages { get; }

        public Book(string title, int pages)
        {
            //if (string.IsNullOrWhiteSpace(title))
            //    throw new ArgumentException("Title is required.", nameof(title));
            //ChangeTitle(title);
            if (pages <= 0)
                throw new ArgumentOutOfRangeException(nameof(pages), "Pages must be greater than 0.");

            Title = title;
            Pages = pages;
        }

        //public void ChangeTitle(string title)
        //{
        //    if (string.IsNullOrWhiteSpace(title))
        //        throw new ArgumentException("Title is required.", nameof(title));
        //    Title = title.Trim();
        //}
    }
}
