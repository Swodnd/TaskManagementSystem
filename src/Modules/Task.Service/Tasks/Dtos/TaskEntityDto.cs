using Tasks.Service.Tasks.Models;

namespace Tasks.Service.Tasks.Dtos
{
    public record TaskEntityDto(int Id, string Name, string Description, Status Status, string? AssignedTo);
}
