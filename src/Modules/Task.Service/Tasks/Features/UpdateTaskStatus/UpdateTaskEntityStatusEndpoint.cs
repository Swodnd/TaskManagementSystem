using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Tasks.Service.Tasks.Models;

namespace Tasks.Service.Tasks.Features.UpdateTaskStatus
{
    public record UpdateTaskEntityStatusRequest(Status NewStatus);
    public record UpdateTaskEntityStatusResponse(bool IsSuccess);
    public class UpdateTaskEntityStatusEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPatch("/tasks/{id:int}/status", async (int id, UpdateTaskEntityStatusRequest request, ISender sender) =>
            {
                var command =  new UpdateTaskEntityStatusCommand(id, request.NewStatus);
                var result = await sender.Send(command);
                var response = result.Adapt<UpdateTaskEntityStatusResponse>();

                return Results.Ok(response);
            })
            .WithName("UpdateTaskStatus")
            .Produces<UpdateTaskEntityStatusResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Update Task Status")
            .WithDescription("Update Task Status");
        }
    }
}
