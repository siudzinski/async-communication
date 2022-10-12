using Microsoft.AspNetCore.SignalR;

namespace Notifications.Hubs;

public interface IAchievementHub
{
    Task AchievementStateChanged(Guid achievementId, string state);
}

public class AchievementHub : Hub<IAchievementHub> { }
