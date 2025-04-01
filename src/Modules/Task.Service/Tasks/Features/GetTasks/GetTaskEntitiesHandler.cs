using Mapster;
using Microsoft.EntityFrameworkCore;
using Shared.CQRS;
using Shared.Pagination;
using Tasks.Service.Data;
using Tasks.Service.Tasks.Dtos;

namespace Tasks.Service.Tasks.Features.GetTasks
{
    public record GetTaskEntitiesQuery(PaginationRequest PaginationRequest)
    : IQuery<GetTaskEntitiesResult>;
    public record GetTaskEntitiesResult(PaginatedResult<TaskEntityDto> Tasks);
    internal class GetTaskEntitiesHandler(TasksServiceDbContext dbContext) : IQueryHandler<GetTaskEntitiesQuery, GetTaskEntitiesResult>
    {
        public async Task<GetTaskEntitiesResult> Handle(GetTaskEntitiesQuery query, CancellationToken cancellationToken)
        {

            var pageIndex = query.PaginationRequest.PageIndex;
            var pageSize = query.PaginationRequest.PageSize;    
            var totalCount = await dbContext.TaskEntities.LongCountAsync(cancellationToken);    

            var taskEntities =await dbContext.TaskEntities
                .AsNoTracking()
                .OrderBy(p => p.Name)
                .Skip(pageIndex * pageSize) 
                .Take(pageSize) 
                .ToListAsync(cancellationToken);

            var taskEntityDtos = taskEntities.Adapt<List<TaskEntityDto>>();

            return new GetTaskEntitiesResult(new PaginatedResult<TaskEntityDto>(pageIndex, pageSize, totalCount, taskEntityDtos));
        }
    }
}
