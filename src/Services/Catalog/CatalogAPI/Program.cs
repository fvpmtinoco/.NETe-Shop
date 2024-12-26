using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions.Handle;
using BuildingBlocks.Extensions.Carter;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

System.Reflection.Assembly assembly = typeof(Program).Assembly;

string? connectionString = builder.Configuration.GetConnectionString("Database");
// Add services to the container
builder.Services.AddCarterWithAssemblies(assembly);
builder.Services.AddMarten(opt =>
{
    opt.Connection(connectionString!);
}).UseLightweightSessions();

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks().AddNpgSql(connectionString!);

WebApplication app = builder.Build();

// Configure the HTTP request pipeline
app.MapCarter();

app.UseExceptionHandler(options => { });

app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

app.Run();
