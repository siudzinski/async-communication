using Achievements.Domain.Infrastructure;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Achievements.Domain;

public static class AchievementsConfig
{
    public static void AddAchievements(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(typeof(AchievementsConfig));

        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq();
        });

        services.AddDbContext<AchievementsContext>(options => 
            options.UseNpgsql(configuration.GetConnectionString("postgres"))); //"Server=localhost;Port=5432;User Id=postgres;Password=password;"

        services.AddHostedService<OutboxBackgroundService>();
    }
}
