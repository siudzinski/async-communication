using Achievements.Contracts;
using MassTransit;

namespace Notifications.Consumers
{
    public class AchievementCompletedConsumer: IConsumer<AchievementCompleted>
    {
        private readonly ILogger<AchievementCompletedConsumer> _logger;

        public AchievementCompletedConsumer(ILogger<AchievementCompletedConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<AchievementCompleted> context)
        {
            _logger.LogInformation($"Message consumed: {context.Message.Id}");

            return Task.CompletedTask;
        }
    }
}