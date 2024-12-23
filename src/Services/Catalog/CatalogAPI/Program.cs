using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions.Handle;
using BuildingBlocks.Extensions.Carter;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

System.Reflection.Assembly assembly = typeof(Program).Assembly;

// Add services to the container
builder.Services.AddCarterWithAssemblies(assembly);
builder.Services.AddMarten(opt =>
{
    opt.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline
app.MapCarter();

app.UseExceptionHandler(options => { });

app.Run();
