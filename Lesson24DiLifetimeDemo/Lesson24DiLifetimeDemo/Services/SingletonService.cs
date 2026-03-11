namespace Lesson24DiLifetimeDemo.Services
{
    public class SingletonService
    {
        public Guid InstanceId { get; } = Guid.NewGuid();
    }
}
