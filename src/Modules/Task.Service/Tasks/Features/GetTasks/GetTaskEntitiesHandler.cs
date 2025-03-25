using Mapster;
using Microsoft.EntityFrameworkCore;
using Shared.CQRS;
using Tasks.Service.Data;
using Tasks.Service.Tasks.Dtos;

namespace Tasks.Service.Tasks.Features.GetTasks
{
    public record GetTaskEntitiesQuery()
    : IQuery<GetTaskEntitiesResult>;
    public record GetTaskEntitiesResult(IEnumerable<TaskEntityDto> Tasks);
    internal class GetTaskEntitiesHandler(TasksServiceDbContext dbContext) : IQueryHandler<GetTaskEntitiesQuery, GetTaskEntitiesResult>
    {
        public async Task<GetTaskEntitiesResult> Handle(GetTaskEntitiesQuery query, CancellationToken cancellationToken)
        {
            var taskEntities =await dbContext.TaskEntities
                .AsNoTracking()
                .OrderBy(p => p.Name)
                .ToListAsync(cancellationToken);

            var taskEntityDtos = taskEntities.Adapt<List<TaskEntityDto>>();

            return new GetTaskEntitiesResult(taskEntityDtos);
        }
    }
}
