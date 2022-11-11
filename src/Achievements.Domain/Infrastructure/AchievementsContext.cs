using Microsoft.EntityFrameworkCore;
using Shared.Outbox;

namespace Achievements.Domain.Infrastructure;

public class AchievementsContext : DbContext
{
    public DbSet<Achievement> Achievements { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }

    public AchievementsContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Achievement>()
            .ToTable("Achievements", "achievements")
            .HasKey(_ => _.Id);

        modelBuilder.Entity<OutboxMessage>()
            .ToTable("OutboxMessages", "achievements")
            .HasKey(_ => _.Id);
    }
}
