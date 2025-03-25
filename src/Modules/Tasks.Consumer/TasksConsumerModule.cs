using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System.Reflection;

namespace Tasks.Consumer
{
    
    public static class TasksConsumerModule
    {
        public static IServiceCollection AddTaskConsumerModule(this IServiceCollection services,
        IConfiguration configuration)
        {
            // Add services to the container.

            // Api Endpoint services

            // Application Use Case services       

            // Data - Infrastructure services

            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });


            return services;
        }

        public static IApplicationBuilder UseTasksConsumerModule(this IApplicationBuilder app)
        {
            // Configure the HTTP request pipeline.

            // 1. Use Api Endpoint services

            // 2. Use Application Use Case services

            // 3. Use Data - Infrastructure services  
            return app;
        }
    }
}
