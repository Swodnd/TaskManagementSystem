using MediatR;
using Shared.CQRS;
using Tasks.Service.Data;
using Tasks.Service.Tasks.Models;

namespace Tasks.Service.Tasks.Features.UpdateTaskStatus
{
    public record UpdateTaskEntityStatusCommand(int TaskId, Status NewStatus) : ICommand<UpdateTaskEntityStatusResult>;
    public record UpdateTaskEntityStatusResult(bool IsSuccess);

    internal class UpdateTaskEntityStatusCommandHandler(TasksServiceDbContext dbContext) : ICommandHandler<UpdateTaskEntityStatusCommand, UpdateTaskEntityStatusResult>
    {
        public async Task<UpdateTaskEntityStatusResult> Handle(UpdateTaskEntityStatusCommand command, CancellationToken cancellationToken)
        {
            var taskEntity = await dbContext.TaskEntities
          .FindAsync([command.TaskId], cancellationToken: cancellationToken);

            if (taskEntity is null)
            {
                throw new Exception($"Task not found: {command.TaskId}");
            }

            UpdatewTaskEntityStatus(taskEntity, command.NewStatus);

            dbContext.Update(taskEntity);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new UpdateTaskEntityStatusResult(true);
        }

        private void UpdatewTaskEntityStatus(TaskEntity taskEntity, Status newStatus)
        {
            taskEntity.UpdateStatus(newStatus);
        }

    }

}
