using Achievements.Contracts;
using MassTransit;

namespace Notifications.Achievements.Consumers
{
    public class AchievementUnlockedConsumer : IConsumer<AchievementUnlocked>
    {
        private readonly ILogger<AchievementUnlockedConsumer> _logger;

        public AchievementUnlockedConsumer(ILogger<AchievementUnlockedConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<AchievementUnlocked> context)
        {
            _logger.LogInformation($"Message consumed: Achievement {context.Message.Id} unlocked.");

            return Task.CompletedTask;
        }
    }
}