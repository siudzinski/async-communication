using Achievements.Infrastructure;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(typeof(Program));

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq();
});

builder.Services.AddDbContext<AchievementsContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("postgres"))); //"Server=localhost;Port=5432;User Id=postgres;Password=password;"

builder.Services.AddHostedService<OutboxBackgroundService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
