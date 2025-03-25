using MassTransit;
using Shared.CQRS;
using Shared.Messaging.Events;

namespace Tasks.Consumer.Features.UpdateTaskStatusLogic
{
    public record UpdateTaskStatusLogicCommand(int TaskId, string NewStatus)
    : ICommand<UpdateTaskStatusLogicResult>;
    public record UpdateTaskStatusLogicResult(bool IsSuccess);
    internal class UpdateTaskStatusLogicHandler(IBus bus) : ICommandHandler<UpdateTaskStatusLogicCommand, UpdateTaskStatusLogicResult>
    {
        public async Task<UpdateTaskStatusLogicResult> Handle(UpdateTaskStatusLogicCommand request, CancellationToken cancellationToken)
        {
            // Business logic goes here
            var errorRandom = new Random().Next(0, 10);
            if (errorRandom < 4)
            {
                return new UpdateTaskStatusLogicResult(false);
            }

            var eventMessage = new TaskStatusBusinessLogicProcessedIntegrationEvent(request.TaskId);

            await bus.Publish(eventMessage, cancellationToken);

            return new UpdateTaskStatusLogicResult(true);
        }
    }
    
}
