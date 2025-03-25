using MediatR;

namespace Shared.Abstractions
{
    public interface IDomainEvent : INotification
    {
        Guid EventId => new Guid();
        public DateTime OccuredOn => DateTime.Now;
        public string EventType => GetType().AssemblyQualifiedName!;
    }
}
