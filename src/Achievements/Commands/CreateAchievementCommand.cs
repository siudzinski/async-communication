using Achievements.Contracts;
using Achievements.Domain;
using Achievements.Infrastructure;
using MediatR;
using Shared.CQRS;
using Shared.Outbox;

namespace Achievements.Commands;

public class CreateAchievementCommand : ICommand
{
    public CreateAchievementCommand() { }
}

public class CreateAchievementCommandHandler : ICommandHandler<CreateAchievementCommand>
{
    private readonly ILogger<CreateAchievementCommandHandler> _logger;
    private readonly AchievementsContext _achievementsContext;

    public CreateAchievementCommandHandler(
        ILogger<CreateAchievementCommandHandler> logger,
        AchievementsContext achievementsContext)
    {
        _logger = logger;
        _achievementsContext = achievementsContext;
    }

    public async Task<Unit> Handle(CreateAchievementCommand command, CancellationToken cancellationToken)
    {
        var achievement = Achievement.CreateNew();

        await _achievementsContext.Achievements.AddAsync(achievement);

        var outboxMessage = OutboxMessage.Create(new AchievementCreated { Id = achievement.Id });
        await _achievementsContext.OutboxMessages.AddAsync(outboxMessage);

        await _achievementsContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
