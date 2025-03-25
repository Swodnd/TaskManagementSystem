using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Tasks.Service.Tasks.Dtos;

namespace Tasks.Service.Tasks.Features.CreateTask
{
    public record CreateTaskEntityRequest(TaskEntityDto TaskEntity);
    public record CreateTaskEntityResponse(int Id);
    public class CreateTaskEntityEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/tasks", async (CreateTaskEntityRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateTaskEntityCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<CreateTaskEntityResponse>();

                return Results.Created($"/tasks/{response.Id}", response);
            })
             .WithName("CreateTask")
             .Produces<CreateTaskEntityResponse>(StatusCodes.Status201Created)
             .ProducesProblem(StatusCodes.Status400BadRequest)
             .WithSummary("Create Task")
             .WithDescription("Create Task");
        }
    }
}
