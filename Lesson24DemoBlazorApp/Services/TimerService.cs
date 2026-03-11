namespace Lesson24DemoBlazorApp.Services
{
    public class TimeService
    {
        public string GetCurrentTime() => DateTime.Now.ToString("T");
    }

}
