using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Shared.Messaging.Events;
using Tasks.Consumer.Features.UpdateTaskStatusLogic;

namespace Tasks.Consumer.Consumer.EventHandlers
{
    public class TaskStatusChangedIntegrationEventHandler(ISender sender, ILogger<TaskStatusChangedIntegrationEventHandler> logger) : IConsumer<TaskStatusChangedIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<TaskStatusChangedIntegrationEvent> context)
        {
            logger.LogInformation("Integration Event: {IntegrationEvent}", context.Message.GetType().Name);

            var command = new UpdateTaskStatusLogicCommand(context.Message.TaskId, context.Message.NewStatus);
            var result = await sender.Send(command);

            if (!result.IsSuccess)
            {
                logger.LogError("Task Status Business Logic Id: {TaskId}", context.Message.TaskId);
                throw new Exception("Test Exception");
            }

            logger.LogInformation("Task Status Business Logic Id: {TaskId} completed.", context.Message.TaskId);
        }
    }
}
