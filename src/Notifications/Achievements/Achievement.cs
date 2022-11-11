namespace Notifications.Achievements;

public class Achievement
{
    public Guid Id { get; }
    public DateTime CreateDate { get; private set; }
    public DateTime? NotificationSendDate { get; private set; }

    private Achievement() { }

    public Achievement(Guid id)
    {
        Id = id;
        CreateDate = DateTime.UtcNow;
        NotificationSendDate = null;
    }

    public void SetNotificationSendDateToNow()
    {
        NotificationSendDate = DateTime.UtcNow;
    }
}
