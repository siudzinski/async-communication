namespace Achievements.Domain;

public class Achievement
{
    public Guid Id { get; }
    public bool Unlocked { get; private set; }
    public DateTime? UnlockDate { get; private set; }

    private Achievement() { }

    private Achievement(Guid id)
    {
        Id = id;
    }

    public static Achievement CreateNew()
    {
        return new Achievement(Guid.NewGuid());
    }

    public void Unlock()
    {
        Unlocked = true;
        UnlockDate = DateTime.UtcNow;
    }
}
