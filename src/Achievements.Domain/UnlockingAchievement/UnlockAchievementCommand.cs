using Achievements.Contracts;
using Achievements.Domain.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shared.CQRS;
using Shared.Outbox;

namespace Achievements.Domain.UnlockingAchievement;

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

        var outboxMessage = OutboxMessage.Create(new AchievementUnlocked { Id = command.AchievementId });
        await _achievementsContext.OutboxMessages.AddAsync(outboxMessage, cancellationToken);

        await _achievementsContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }

    private Task<Achievement> GetAchievement(Guid achievementId) 
        => _achievementsContext.Achievements.FirstOrDefaultAsync(_ => _.Id == achievementId);
}
