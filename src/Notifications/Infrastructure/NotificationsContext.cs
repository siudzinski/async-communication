using Microsoft.EntityFrameworkCore;
using Notifications.Achievements;

namespace Notifications.Infrastructure;

public class NotificationsContext : DbContext
{
    public DbSet<Achievement> Achievements { get; set; }

    public NotificationsContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Achievement>()
            .ToTable("Achievements", "notifications")
            .HasKey(_ => _.Id);
    }
}
