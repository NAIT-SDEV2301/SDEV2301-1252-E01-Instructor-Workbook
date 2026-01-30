using System;
using System.Collections.Generic;
using System.Text;

namespace L09_TddDomain
{
    public class Person
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public string? Email { get; set; }

        public string FullName => $"{LastName}, {FirstName}";

        // Default constructor
        public Person()
        {
            //FirstName = "Unknown";
            ChangeFirstName("Unknown");
            LastName = "Unknown";
        }
        
        public Person(string firstName, string lastName, string? email)
        {
            //FirstName = firstName.Trim();
            ChangeFirstName(firstName);
            LastName = lastName.Trim();
            Email = email;
        }

        public void ChangeFirstName(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new ArgumentNullException(nameof(firstName),"First Name is required");
            }
            FirstName = firstName.Trim();
        }

    }
}
