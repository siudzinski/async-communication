using Achievements.Contracts;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Notifications.Achievements;
using Notifications.Hubs;

namespace Notifications.Consumers
{
    public class AchievementCompletedConsumer: IConsumer<IAchievementCompleted>
    {
        private readonly ILogger<AchievementCompletedConsumer> _logger;
        private readonly IHubContext<AchievementHub, IAchievementHub> _hubContext;

        public AchievementCompletedConsumer(
            ILogger<AchievementCompletedConsumer> logger, 
            IHubContext<AchievementHub, IAchievementHub> hubContext)
        {
            _logger = logger;
            _hubContext = hubContext;
        }

        public async Task Consume(ConsumeContext<IAchievementCompleted> context)
        {
            _logger.LogInformation($"Message consumed: {context.Message.Id}");

            await _hubContext.Clients.All.AchievementStateChanged(context.Message.Id, States.Completed.ToString());
        }
    }
}