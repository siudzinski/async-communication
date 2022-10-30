using Achievements.Contracts;
using MassTransit;

namespace Notifications.Consumers
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
            _logger.LogInformation($"Message consumed: {context.Message.Id}");

            return Task.CompletedTask;
        }
    }
}