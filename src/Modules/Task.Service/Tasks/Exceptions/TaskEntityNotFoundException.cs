using Shared.Exceptions;

namespace Tasks.Service.Tasks.Exceptions
{
    public class TaskEntityNotFoundException: NotFoundException
    {
        public TaskEntityNotFoundException(int taskId)
            : base("Task", taskId)
        {
        }
    }
}
