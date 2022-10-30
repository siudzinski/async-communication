using System.Text.Json;
using Achievements.Contracts;
using Achievements.Domain;
using Achievements.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.CQRS;
using Shared.Outbox;

namespace Achievements.Commands;

public class UnlockAchievementCommand : ICommand
{
    public Guid AchievementId { get; }

    public UnlockAchievementCommand(Guid achievementId)
    {
        AchievementId = achievementId;
    }
}

public class UnlockAchievementCommandHandler : ICommandHandler<UnlockAchievementCommand>
{
    private readonly ILogger<UnlockAchievementCommandHandler> _logger;
    private readonly AchievementsContext _achievementsContext;

    public UnlockAchievementCommandHandler(
        ILogger<UnlockAchievementCommandHandler> logger,
        AchievementsContext achievementsContext)
    {
        _logger = logger;
        _achievementsContext = achievementsContext;
    }

    public async Task<Unit> Handle(UnlockAchievementCommand command, CancellationToken cancellationToken)
    {
        var achievement = await GetAchievement(command.AchievementId);

        achievement.Unlock();

        var outboxMessage = new OutboxMessage(
            DateTime.UtcNow, 
            typeof(AchievementUnlocked).FullName,
            JsonSerializer.Serialize(new AchievementUnlocked { Id = command.AchievementId }));

        await _achievementsContext.OutboxMessages.AddAsync(outboxMessage);

        await _achievementsContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }

    private Task<Achievement> GetAchievement(Guid achievementId) 
        => _achievementsContext.Achievements.FirstOrDefaultAsync(_ => _.Id == achievementId);
}
