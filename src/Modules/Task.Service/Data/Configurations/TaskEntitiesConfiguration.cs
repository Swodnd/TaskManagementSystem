using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Tasks.Service.Tasks.Models;

namespace Tasks.Service.Data.Configurations
{
    public class TaskEntitiesConfiguration : IEntityTypeConfiguration<TaskEntity>
    {
        public void Configure(EntityTypeBuilder<TaskEntity> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.Status)
                .HasConversion(v => v.ToString(),v => (Status)Enum.Parse(typeof(Status), v))
                .IsRequired();
            builder.Property(p => p.Description).IsRequired();
            builder.Property(p => p.AssignedTo);
        }
    }
}
