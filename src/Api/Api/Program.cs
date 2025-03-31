using Carter;
using Shared.Exceptions.Handler;
using Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

var tasksServiceAssembly = typeof(TasksServiceModule).Assembly;
var tasksConsumerAssembly = typeof(TasksConsumerModule).Assembly;
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCarterWithAssemblies(tasksServiceAssembly);

builder.Services.AddMassTransitWithAssemblies(builder.Configuration, tasksServiceAssembly, tasksConsumerAssembly);

builder.Services
    .AddTaskServiceModule(builder.Configuration)
    .AddTaskConsumerModule(builder.Configuration);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

//For swagger
builder.Services.AddControllers().AddJsonOptions(opts =>
    opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();


app.UseExceptionHandler(option => { });

app.UseTasksServiceModule();

app.MapCarter();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();

