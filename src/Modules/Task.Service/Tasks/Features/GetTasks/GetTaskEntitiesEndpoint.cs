using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Tasks.Service.Tasks.Dtos;

namespace Tasks.Service.Tasks.Features.GetTasks
{
    public record  GetTaskEntitiesResponse(List<TaskEntityDto> Tasks);

    public class GetTaskEntitiesEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/tasks", async (ISender sender) =>
            {
                var result = await sender.Send(new GetTaskEntitiesQuery());
                var response = result.Adapt<GetTaskEntitiesResponse>();

                return Results.Ok(response);

            })
            .WithName("GetTasks")
            .Produces<GetTaskEntitiesResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Tasks")
            .WithDescription("Get Tasks");
        }
    }
}
