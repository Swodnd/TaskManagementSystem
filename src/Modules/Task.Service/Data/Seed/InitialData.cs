using Tasks.Service.Tasks.Models;

namespace Tasks.Service.Data.Seed
{
    public static class InitialData
    {
        public static IEnumerable<TaskEntity> Tasks =>
            new List<TaskEntity>
            {
                TaskEntity.Create(0,"Task Name A", "Task Name A description", Status.NotStarted, "User A"),
                TaskEntity.Create(0,"Task Name B", "Task Name B description", Status.NotStarted, "User A"),
                TaskEntity.Create(0,"Task Name D", "Task Name D description", Status.NotStarted, "User B"),
                TaskEntity.Create(0,"Task Name E", "Task Name E description", Status.NotStarted, "User B"),
                TaskEntity.Create(0,"Task Name F", "Task Name F description", Status.NotStarted, null),
            };
    }
}
