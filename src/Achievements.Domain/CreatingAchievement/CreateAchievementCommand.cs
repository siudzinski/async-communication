using Achievements.Contracts;
using Achievements.Domain.Infrastructure;
using MediatR;
using Microsoft.Extensions.Logging;
using Shared.CQRS;
using Shared.Outbox;

namespace Achievements.Domain.CreatingAchievement;

public class CreateAchievementCommand : ICommand
{
    public Guid Id { get; }
    public CreateAchievementCommand()
    {
        Id = Guid.NewGuid();
    }
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
        var achievement = Achievement.CreateNew(command.Id);

        await _achievementsContext.Achievements.AddAsync(achievement, cancellationToken);

        var outboxMessage = OutboxMessage.Create(new AchievementCreated { Id = achievement.Id });
        await _achievementsContext.OutboxMessages.AddAsync(outboxMessage, cancellationToken);

        await _achievementsContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
