using Shared.Abstractions;

namespace Tasks.Service.Tasks.Events
{
    public record TaskEntityCreatedEvent(Models.TaskEntity Task) : IDomainEvent;
}
