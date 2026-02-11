/* Imperative Code */
// Create a list of int numbers
var numbers = new List<int>() { 1,2,3,4,5,6};
//var evens = new List<int>();
//foreach (var number in numbers)
//{
//    if (number % 2 == 0)
//    {
//        evens.Add(number); 
//    }
//}
// What is the input -- `numbers`
// What is the condition -- `number % 2 == 0`
// What is the output -- `evens`
// input --> condition --> output
/* LINQ version*/
var evens = numbers
                .Where(number => number % 2 == 0)
                .ToList();
// Key Idea:
//  1) LINQ works on `IEnumerable<T>`
//  2) LINQ uses extension methods
//  Todays core operators:
//      1)  `Where` -> filters items
//      2)  `Select` -> changes shape

var names = new List<string>() {"Hinda","Liam","Derek","Kia","Sam","Siji" };

var sNames = names.Where(name => name.StartsWith("S"));
var upper = names.Select(n => n.ToUpper());

Module02.Lesson13.LinqDemo.Run();

