using System;
using System.Collections.Generic;
using System.Text;

namespace Module02.Lesson14.LinqAdvanceDemo
{
    public class Student
    {
        public string Name { get; set; }
        public int Grade { get; set; }   // 0–100
        public string Section { get; set; } // e.g., "A01", "E01", "OE01"
    }

    public class LinqAdvanceDemoProgram
    {
        public static void Run()
        {
            var students = new List<Student>
        {
            new Student { Name = "Alice",   Grade = 85, Section = "A01" },
            new Student { Name = "Bob",     Grade = 72, Section = "A01" },
            new Student { Name = "Charlie", Grade = 90, Section = "OE01"},
            new Student { Name = "Diana",   Grade = 60, Section = "E01" },
            new Student { Name = "Evan",    Grade = 95, Section = "OE01"},
            new Student { Name = "Farah",   Grade = 72, Section = "E01" },
            new Student { Name = "Gavin",   Grade = 88, Section = "A01" }
        };

            Console.WriteLine("=== LINQ Advanced Demo ===");

            // ------------------------------------------------------------
            // 1) FILTERING (Where): passing students (>= 70)
            // ------------------------------------------------------------
            var passing = students.Where(s => s.Grade >= 70);

            Console.WriteLine("\n[Filtering] Passing students (Grade >= 70):");
            foreach (var s in passing)
                Console.WriteLine($"{s.Name,-7}  {s.Grade}");

            // ------------------------------------------------------------
            // 2) SORTING (OrderBy/ThenBy): by Grade desc, then Name asc
            // ------------------------------------------------------------
            var sorted = students
                .OrderByDescending(s => s.Grade)
                .ThenBy(s => s.Name);

            Console.WriteLine("\n[Sorting] By grade (desc), then name:");
            foreach (var s in sorted)
                Console.WriteLine($"{s.Grade,3}  {s.Name}");

            // ------------------------------------------------------------
            // 3) PROJECTION (Select): shape data into new forms
            //    a) Names only
            //    b) Anonymous type with Name + Pass/Fail flag
            // ------------------------------------------------------------
            var namesOnly = students.Select(s => s.Name);

            Console.WriteLine("\n[Projection] Names only:");
            foreach (var name in namesOnly)
                Console.WriteLine(name);

            var nameAndStatus = students.Select(s => new
            {
                s.Name,
                IsPass = s.Grade >= 70
            });

            Console.WriteLine("\n[Projection] Name + status (IsPass):");
            foreach (var row in nameAndStatus)
                Console.WriteLine($"{row.Name,-7}  Pass? {row.IsPass}");

            // ------------------------------------------------------------
            // 4) AGGREGATION: Count, Average, Min, Max, Sum
            //    Also: count students above average
            // ------------------------------------------------------------
            int countAll = students.Count();
            double avgGrade = students.Average(s => s.Grade);
            int minGrade = students.Min(s => s.Grade);
            int maxGrade = students.Max(s => s.Grade);
            int sumOfGrades = students.Sum(s => s.Grade);
            int aboveAverage = students.Count(s => s.Grade > avgGrade);

            Console.WriteLine("\n[Aggregation] Summary stats:");
            Console.WriteLine($"Count = {countAll}");
            Console.WriteLine($"Average = {avgGrade:F1}");
            Console.WriteLine($"Min = {minGrade}, Max = {maxGrade}");
            Console.WriteLine($"Sum = {sumOfGrades}");
            Console.WriteLine($"Students above average = {aboveAverage}");

            // ------------------------------------------------------------
            // COMBINED QUERY: Filter -> Sort -> Project
            // Passing students in OE01, sorted by grade desc, show Name + Grade
            // ------------------------------------------------------------
            var report = students
                .Where(s => s.Section == "OE01" && s.Grade >= 70)
                .OrderByDescending(s => s.Grade)
                .Select(s => new { s.Name, s.Grade });

            Console.WriteLine("\n[Combined] Passing in OE01 (desc by grade):");
            foreach (var row in report)
                Console.WriteLine($"{row.Name,-7}  {row.Grade}");

            // OPTIONAL: Query syntax equivalent for the combined query
            var reportQuery =
                from s in students
                where s.Section == "OE01" && s.Grade >= 70
                orderby s.Grade descending
                select new { s.Name, s.Grade };

            Console.WriteLine("\n[Combined - Query Syntax] Passing in OE01:");
            foreach (var row in reportQuery)
                Console.WriteLine($"{row.Name,-7}  {row.Grade}");
        }
    }
}
