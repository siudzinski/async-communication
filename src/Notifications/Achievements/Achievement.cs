namespace Notifications.Achievements;

public class Achievement
{
    public Guid Id { get; }
    public DateTime CreateDate { get; private set; }

    private Achievement() { }

    public Achievement(Guid id)
    {
        Id = id;
        CreateDate = DateTime.UtcNow;
    }
}
