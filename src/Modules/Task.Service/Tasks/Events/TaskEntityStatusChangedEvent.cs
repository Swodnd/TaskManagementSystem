using Shared.Abstractions;
using Tasks.Service.Tasks.Models;

namespace Tasks.Service.Tasks.Events
{
    public record TaskEntityStatusChangedEvent(int TaskId, Status NewStatus ) : IDomainEvent;
}
