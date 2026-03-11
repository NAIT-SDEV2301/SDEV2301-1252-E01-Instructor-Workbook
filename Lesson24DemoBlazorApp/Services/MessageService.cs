namespace Lesson24DemoBlazorApp.Services
{
    public class MessageService
    {
        public string GetGreeting() => "Hello from service!";

        private static readonly string[] Quotes =
        {
            "Stay curious.",
            "Write meaningful code.",
            "Refactor early, refactor often."
        };

        public string GetRandomQuote()
        {
            var random = new Random();
            return Quotes[random.Next(Quotes.Length)];
        }

    }

}
