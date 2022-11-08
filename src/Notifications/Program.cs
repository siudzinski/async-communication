using System.Reflection;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notifications.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(typeof(Program));

builder.Services.AddMassTransit(x =>
{
    x.AddConsumers(Assembly.GetExecutingAssembly());
    x.UsingRabbitMq((context, config) => config.ConfigureEndpoints(context));
});

builder.Services.AddDbContext<NotificationsContext>(options => 
    options.UseNpgsql("Server=localhost;Port=5432;User Id=postgres;Password=password;"));

var app = builder.Build();

app.Run();
