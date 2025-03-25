using FluentValidation;
using Shared.CQRS;
using Tasks.Service.Data;
using Tasks.Service.Tasks.Dtos;
using Tasks.Service.Tasks.Models;

namespace Tasks.Service.Tasks.Features.CreateTask
{
    public record CreateTaskEntityCommand(TaskEntityDto TaskEntity) : ICommand<CreateTaskEntityResult>;
    public record CreateTaskEntityResult(int Id);

    public class CreateTaskEntityCommandValidator : AbstractValidator<CreateTaskEntityCommand>
    {
        public CreateTaskEntityCommandValidator()
        {
            RuleFor(x => x.TaskEntity.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.TaskEntity.Description).NotEmpty().WithMessage("Description is required");
            RuleFor(x => x.TaskEntity.Status).IsInEnum().WithMessage("Status out of range");
        }
    }

    internal class CreateTaskCommandHandler(TasksServiceDbContext dbContext) : ICommandHandler<CreateTaskEntityCommand, CreateTaskEntityResult>
    {
        public async Task<CreateTaskEntityResult> Handle(CreateTaskEntityCommand command, CancellationToken cancellationToken)
        {
            var taskEntity = CreateNewTaskEntity(command.TaskEntity);

            dbContext.TaskEntities.Add(taskEntity);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new CreateTaskEntityResult(taskEntity.Id);
        }

        private TaskEntity CreateNewTaskEntity(TaskEntityDto taskEntityDto)
        {
            var taskEntity = TaskEntity.Create(0, taskEntityDto.Name, taskEntityDto.Description, taskEntityDto.Status, taskEntityDto.AssignedTo);

            return taskEntity;
        }
    }
}
