namespace Achievements.Domain;

public class Achievement
{
    public Guid Id { get; }
    public bool Unlocked { get; private set; }
    public DateTime? UnlockDate { get; private set; }

    private Achievement() { }

    public Achievement(Guid id)
    {
        Id = id;
    }

    public void Unlock()
    {
        Unlocked = true;
        UnlockDate = DateTime.UtcNow;
    }
}
