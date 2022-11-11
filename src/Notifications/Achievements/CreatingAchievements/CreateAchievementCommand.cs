using MediatR;
using Notifications.Infrastructure;
using Shared.CQRS;

namespace Notifications.Achievements.CreatingAchievements;

public class CreateAchievementCommand : ICommand
{
    public Guid Id { get; }

    public CreateAchievementCommand(Guid id)
    {
        Id = id;
    }
}

public class CreateAchievementCommandHandler : ICommandHandler<CreateAchievementCommand>
{
    private readonly ILogger<CreateAchievementCommandHandler> _logger;
    private readonly NotificationsContext _notificationsContext;

    public CreateAchievementCommandHandler(
        ILogger<CreateAchievementCommandHandler> logger,
        NotificationsContext notificationsContext)
    {
        _logger = logger;
        _notificationsContext = notificationsContext;
    }

    public async Task<Unit> Handle(CreateAchievementCommand command, CancellationToken cancellationToken)
    {
        var achievement = new Achievement(command.Id);

        await _notificationsContext.Achievements.AddAsync(achievement, cancellationToken);
        await _notificationsContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
