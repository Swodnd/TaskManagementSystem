using Microsoft.EntityFrameworkCore;
using Shared.Data.Seed;

namespace Tasks.Service.Data.Seed
{
    public class TasksServiceDataSeeder(TasksServiceDbContext dbContext) : IDataSeeder
    {
        public async System.Threading.Tasks.Task SeedAsync()
        {
            if (!await dbContext.TaskEntities.AnyAsync())
            {
                await dbContext.TaskEntities.AddRangeAsync(InitialData.Tasks);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
