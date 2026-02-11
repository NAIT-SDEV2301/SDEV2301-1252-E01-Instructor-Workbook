using Module02.Lesson14.Activity1;

var students = new List<Student>
        {
            new Student { Name = "Alice",   Grade = 85 },
            new Student { Name = "Bob",     Grade = 72 },
            new Student { Name = "Charlie", Grade = 90 },
            new Student { Name = "Diana",   Grade = 60 },
            new Student { Name = "Evan",    Grade = 95 }
        };
// How would we list only passing students,
// sorted highest mark first, showing name and grade?

var sortedByMark = students.OrderBy(s => s.Grade);

var sortedDescending = students.OrderByDescending(s => s.Grade);
// Secondary sorting with ThenBy
var sorted = students
    .OrderByDescending(s => s.Grade)
    .ThenBy(s => s.Name);

// Filter -> Sort -> Project

var topStudents = students
    .Where(s => s.Grade >= 80)              // Filter
    .OrderByDescending(s => s.Name)         // Sort
    .Select(s => $"{s.Name} ({s.Grade})");  // Project

//  Grouping
var products = new List<Product>
{
    new Product{Name="Name1", Price=10.00m, Category="Category1"},
    new Product{Name="Name2", Price=20.00m, Category="Category2"},
    new Product{Name="Name3", Price=30.00m, Category="Category2"},
    new Product{Name="Name4", Price=40.00m, Category="Category3"},
    new Product{Name="Name5", Price=50.00m, Category="Category1"},

};
var countsByCategory = products
    .GroupBy(p => p.Category) // returns groups,not individual items
    .Select(g => new            // Each group has a KEY and a collection of items
    {
        Category = g.Key,
        Count = g.Count()
    });

Module02.Lesson14.LinqAdvanceDemo.LinqAdvanceDemoProgram.Run();