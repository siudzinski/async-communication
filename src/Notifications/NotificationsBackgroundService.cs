using System.Data;
using Microsoft.EntityFrameworkCore;
using Notifications.Infrastructure;

namespace Notifications;

public class NotificationsBackgroundService: BackgroundService
{
    private readonly ILogger<NotificationsBackgroundService> _logger;
    private readonly IServiceScopeFactory _scopeFactory;

    private readonly PeriodicTimer _timer = new(TimeSpan.FromSeconds(20));

    public NotificationsBackgroundService(
        ILogger<NotificationsBackgroundService> logger,
        IServiceScopeFactory scopeFactory)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested && await _timer.WaitForNextTickAsync(cancellationToken))
        {
            await SendNotifications(cancellationToken);
        }
    }

    private async Task SendNotifications(CancellationToken cancellationToken)
    {
        using var scope = _scopeFactory.CreateScope();
        var notificationsContext = scope.ServiceProvider.GetRequiredService<NotificationsContext>();

        var achievements = await notificationsContext.Achievements
            .Where(_ => _.NotificationSendDate == null)
            .Take(50)
            .ToListAsync(cancellationToken);

        foreach(var achievement in achievements)
        {
            //TODO make fake http service with retry policy
            _logger.LogInformation($"Notification for {achievement.Id} sent.");

            achievement.SetNotificationSendDateToNow();
            await notificationsContext.SaveChangesAsync();
        }
    }
}