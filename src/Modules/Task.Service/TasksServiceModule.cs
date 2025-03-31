using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Data.Interceptors;
using Shared.Data.Seed;
using Shared.Data;
using System.Reflection;
using Tasks.Service.Data;
using Tasks.Service.Data.Seed;
using Shared.Behaviors;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Tasks.Service
{
    public static class TasksServiceModule
    {
        public static IServiceCollection AddTaskServiceModule(this IServiceCollection services,
        IConfiguration configuration)
        {
            // Add services to the container.

            // Api Endpoint services

            // Application Use Case services       

            // Data - Infrastructure services
            //services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
            //services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

            var connectionString = configuration.GetConnectionString("Database");

            //services.AddDbContext<TasksServiceDbContext>(options =>
            //{
            //    //options.AddInterceptors(new AuditableEntityInterceptor());
            //    //options.AddInterceptors(new DispatchDomainEventsInterceptor());
            //    options.UseNpgsql(connectionString);
            //});

            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
            services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();
            services.AddDbContext<TasksServiceDbContext>((sp, options) =>
            {
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                options.UseNpgsql(connectionString);
            });

            services.AddScoped<IDataSeeder, TasksServiceDataSeeder>();
            services.AddMediatR(config => 
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                config.AddOpenBehavior(typeof(ValidationBehavior<,>));
                config.AddOpenBehavior(typeof(LoggingBehavior<,>));
            });

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }

        public static IApplicationBuilder UseTasksServiceModule(this IApplicationBuilder app)
        {
            // Configure the HTTP request pipeline.

            // 1. Use Api Endpoint services

            // 2. Use Application Use Case services

            // 3. Use Data - Infrastructure services  
            app.UseMigration<TasksServiceDbContext>();

            return app;
        }
    }
}
