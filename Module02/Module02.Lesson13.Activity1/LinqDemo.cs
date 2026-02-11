using System;
using System.Collections.Generic;
using System.Text;

namespace Module02.Lesson13;

public class Student
{
    public string Name { get; set; }
    public int Grade { get; set; }
}

public class LinqDemo
{
    public static void Run()
    {
        var students = new List<Student>
        {
            new Student { Name = "Alice",   Grade = 85 },
            new Student { Name = "Bob",     Grade = 72 },
            new Student { Name = "Charlie", Grade = 90 },
            new Student { Name = "Diana",   Grade = 60 },
            new Student { Name = "Evan",    Grade = 95 }
        };

        Console.WriteLine("=== LINQ Teaching Demo ===");

        // --- STEP 1: Filter (Where) -------------------------------
        // Goal: Keep only students with Grade >= 70 (passing).
        // Method syntax (start here for consistency with lambda expressions later):
        var passing = students.Where(s => s.Grade >= 70);

        Console.WriteLine("\nSTEP 1: Passing students (Grade >= 70):");
        foreach (var s in passing)
            Console.WriteLine($"{s.Name} - {s.Grade}");

        Pause("Discuss: How is this different from a foreach with an if? Predict the next output…");

        // --- STEP 2: Order (OrderByDescending) --------------------
        // Goal: Sort the filtered results by Grade descending.
        var passingSorted = passing.OrderByDescending(s => s.Grade);

        Console.WriteLine("\nSTEP 2: Passing students sorted by grade (desc):");
        foreach (var s in passingSorted)
            Console.WriteLine($"{s.Name} - {s.Grade}");

        Pause("Quick check: Which student appears first? Why?");

        // --- STEP 3: Project (Select) -----------------------------
        // Goal: Project to just the student names (strings).
        var namesOnly = passingSorted.Select(s => s.Name);

        Console.WriteLine("\nSTEP 3: Names of passing students (sorted):");
        foreach (var name in namesOnly)
            Console.WriteLine(name);

        Pause("Extension: How would we project an anonymous type like { Name, IsHonors }? (Hint: Select)");

        // --- OPTIONAL: Query syntax equivalent (show after method syntax) ---
        // Keep this brief—just demonstrate the parallel form once.
        var passingQuery =
            from s in students
            where s.Grade >= 70
            orderby s.Grade descending
            select s.Name;

        Console.WriteLine("\nOPTIONAL (Query Syntax): Names of passing students (sorted):");
        foreach (var name in passingQuery)
            Console.WriteLine(name);

        Console.WriteLine("\nEnd of teaching demo. See LinqDemo_Guided.cs for practice tasks and LinqDemo_Solution.cs for answers.");
    }

    // Small helper to create natural pause points in class.
    private static void Pause(string prompt)
    {
        Console.WriteLine($"\n— {prompt}");
        Console.WriteLine("Press ENTER to continue…");
        Console.ReadLine();
    }
}
