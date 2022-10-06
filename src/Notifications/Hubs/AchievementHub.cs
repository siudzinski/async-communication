using Microsoft.AspNetCore.SignalR;

namespace Notifications.Hubs;

public interface IAchievementHub
{
    Task AchievementUnlocked(Guid achievementId);
}

public class AchievementHub : Hub<IAchievementHub> { }
