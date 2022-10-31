using Achievements.Contracts;
using MassTransit;

namespace Notifications.Consumers
{
    public class AchievementCreatedConsumer: IConsumer<AchievementCreated>
    {
        private readonly ILogger<AchievementCreatedConsumer> _logger;

        public AchievementCreatedConsumer(ILogger<AchievementCreatedConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<AchievementCreated> context)
        {
            _logger.LogInformation($"Message consumed: Achievement {context.Message.Id} created.");

            return Task.CompletedTask;
        }
    }
}