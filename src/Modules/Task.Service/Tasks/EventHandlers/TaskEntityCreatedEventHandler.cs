using MediatR;
using Microsoft.Extensions.Logging;
using System;
using Tasks.Service.Tasks.Events;

namespace Tasks.Service.Tasks.EventHandlers
{
    public  class TaskEntityCreatedEventHandler(ILogger<TaskEntityCreatedEventHandler> logger)
    : INotificationHandler<TaskEntityCreatedEvent>
    {
        public Task Handle(TaskEntityCreatedEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domain Event handled: {DomainEvent}", notification.GetType().Name);
            return Task.CompletedTask;
        }
    }
}
