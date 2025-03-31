using Carter;
using Serilog;
using Shared.Exceptions.Handler;
using Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((context, config) => config.ReadFrom.Configuration(context.Configuration));

var tasksServiceAssembly = typeof(TasksServiceModule).Assembly;
var tasksConsumerAssembly = typeof(TasksConsumerModule).Assembly;
//builder.Logging.ClearProviders();
//builder.Logging.AddConsole();


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




app.UseTasksServiceModule();

app.MapCarter();
app.UseSerilogRequestLogging();
app.UseExceptionHandler(option => { });

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();

