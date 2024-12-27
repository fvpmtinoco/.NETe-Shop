using BasketAPI.Data;
using BasketAPI.Models;
using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions.Handle;
using BuildingBlocks.Extensions.Carter;
using Carter;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container

System.Reflection.Assembly assembly = typeof(Program).Assembly;

// Add services to the container
builder.Services.AddCarterWithAssemblies(assembly);

builder.Services.AddScoped<IBasketRepository, BasketRepository>();

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
    opts.AutoCreateSchemaObjects = Weasel.Core.AutoCreate.CreateOrUpdate;
    opts.Schema.For<ShoppingCart>().Identity(x => x.Username);
}).UseLightweightSessions();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline

app.UseExceptionHandler(opts => { });
app.MapCarter();
app.Run();
