using System.Reflection;
using MassTransit;
using Notifications.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(x =>
{
    x.AddConsumers(Assembly.GetExecutingAssembly());
    x.UsingRabbitMq((context, config) => config.ConfigureEndpoints(context));
});

builder.Services.AddSignalR();

var app = builder.Build();

app.MapHub<AchievementHub>("/achievements");

app.Run();
