using System;
using System.Collections.Generic;
using System.Text;

namespace L05_Demo02
{
    public class Student
    {
        public string Name { get; }
        public int Age { get; }

        public Student(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public void PrintInfo()
        {
            Console.WriteLine($"{Name} is {Age} years old.");
        }
    }
}
