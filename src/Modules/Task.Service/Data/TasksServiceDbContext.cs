using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Tasks.Service.Tasks.Models;
using System.Threading.Tasks;

namespace Tasks.Service.Data
{
    public class TasksServiceDbContext : DbContext
    {
        public TasksServiceDbContext(DbContextOptions<TasksServiceDbContext> options) : base(options)
        {
        }

        public DbSet<TaskEntity> TaskEntities => Set<TaskEntity>();

        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("taskservice");
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
