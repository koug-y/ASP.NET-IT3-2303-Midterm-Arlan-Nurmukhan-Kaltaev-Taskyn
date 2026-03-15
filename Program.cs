using System.Text.Json.Serialization;
using TaskTrackerApi.Infrastructure;
using TaskTrackerApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddSingleton<ITaskRepository>(serviceProvider =>
{
    var logger = serviceProvider.GetRequiredService<ILogger<InMemoryTaskRepository>>();
    return new InMemoryTaskRepository(SeedData.Create(), logger);
});

var app = builder.Build();

app.MapGet("/", () => Results.Redirect("/api/tasks"));
app.MapControllers();

app.Run();
