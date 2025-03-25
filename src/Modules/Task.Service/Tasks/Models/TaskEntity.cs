using Shared.Abstractions;
using Tasks.Service.Tasks.Events;

namespace Tasks.Service.Tasks.Models
{
    public class TaskEntity : Aggregate<int>
    {
        public string Name { get; private set; } = default!;
        public string Description { get; private set; } = default!;
        public Status Status { get; private set; } = default!;
        public string? AssignedTo { get; private set; }

        public static TaskEntity Create(int id, string name, string description, Status status, string? assignedTo)
        {
            ArgumentException.ThrowIfNullOrEmpty(name);
            ArgumentException.ThrowIfNullOrEmpty(description);

            var task = new TaskEntity
            {
                Id = id,
                Name = name,
                Description = description,
                Status = status,
                AssignedTo = assignedTo
            };

            task.AddDomainEvent(new TaskEntityCreatedEvent(task));

            return task;
        }

        public void UpdateStatus(Status status)
        {
            //Raise Domain Event 
            if (Status != status)
            {
                Status = status;
                AddDomainEvent(new TaskEntityStatusChangedEvent(Id, Status));
            }
        }
    }
}
