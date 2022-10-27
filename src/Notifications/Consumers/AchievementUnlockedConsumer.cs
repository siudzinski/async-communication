using Achievements.Contracts;
using MassTransit;

namespace Notifications.Consumers
{
    public class AchievementUnlockedConsumer : IConsumer<IAchievementUnlocked>
    {
        private readonly ILogger<AchievementUnlockedConsumer> _logger;

        public AchievementUnlockedConsumer(ILogger<AchievementUnlockedConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<IAchievementUnlocked> context)
        {
            _logger.LogInformation($"Message consumed: {context.Message.Id}");

            return Task.CompletedTask;
        }
    }
}