using BuildingBlocks.Extensions.Carter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddCarterWithAssemblies(typeof(Program).Assembly);
builder.Services.AddMarten(opt =>
{
    opt.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(typeof(Program).Assembly);
});

var app = builder.Build();

// Configure the HTTP request pipeline
app.MapCarter();

app.Run();
