using Achievements.Contracts;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Notifications.Hubs;

namespace Notifications.Consumers
{
    public class AchievementUnlockedConsumer : IConsumer<IAchievementUnlocked>
    {
        private readonly ILogger<AchievementUnlockedConsumer> _logger;
        private readonly IHubContext<AchievementHub, IAchievementHub> _hubContext;

        public AchievementUnlockedConsumer(
            ILogger<AchievementUnlockedConsumer> logger, 
            IHubContext<AchievementHub, IAchievementHub> hubContext)
        {
            _logger = logger;
            _hubContext = hubContext;
        }

        public async Task Consume(ConsumeContext<IAchievementUnlocked> context)
        {
            _logger.LogInformation($"Message consumed: {context.Message.Id}");

            await _hubContext.Clients.All.AchievementUnlocked(context.Message.Id);
        }
    }
}