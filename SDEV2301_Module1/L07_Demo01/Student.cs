using System;
using System.Collections.Generic;
using System.Text;

namespace L07_Demo01
{
    public class Student
    {
        private int _age;

        public string Name { get; }

        public int Age
        {
            get => _age;
            set
            {
                // value is a reserved keyword that is only accessible
                // inside the set block that contains the new value being assigned
                if (value < 0 || value > 120)
                {
                    throw new ArgumentException("Age must be between 0 and 120.");
                }

                _age = value;
            }
        }

        public Student(string name, int age)
        {
            Name = name;
            Age = age;
        }
    }
}
