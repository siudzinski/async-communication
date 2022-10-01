using System.Reflection;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(x =>
{
    x.AddConsumers(Assembly.GetExecutingAssembly());
    x.UsingRabbitMq((context, config) => config.ConfigureEndpoints(context));
});

var app = builder.Build();

app.Run();
