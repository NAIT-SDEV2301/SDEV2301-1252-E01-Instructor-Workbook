namespace Lesson24DiLifetimeDemo.Services
{
    public class ScopedService
    {
        public Guid InstanceId { get; } = Guid.NewGuid();
    }
}
