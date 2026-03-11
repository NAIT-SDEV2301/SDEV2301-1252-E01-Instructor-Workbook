namespace Lesson24DiLifetimeDemo.Services
{
    public class TransientService
    {
        public Guid InstanceId { get; } = Guid.NewGuid();
    }
}
