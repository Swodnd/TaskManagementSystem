using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Shared.Messaging.Events;
using Tasks.Service.Tasks.Events;

namespace Tasks.Service.Tasks.EventHandlers
{
    public class TaskEntityStatusChangedEventHandler(IBus bus, ILogger<TaskEntityStatusChangedEventHandler> logger) : INotificationHandler<TaskEntityStatusChangedEvent>
    {
        public async Task Handle(TaskEntityStatusChangedEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Task status changed to {Status} for task {TaskId}", notification.NewStatus, notification.TaskId);

            var integrationEvent = new TaskStatusChangedIntegrationEvent
            {
                TaskId = notification.TaskId,
                NewStatus = notification.NewStatus.ToString()
            };

            await bus.Publish(integrationEvent);
        }
    }
}
