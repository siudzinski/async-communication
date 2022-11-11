using Achievements.Contracts;
using MassTransit;
using MediatR;

namespace Notifications.Achievements.CreatingAchievements;

public class AchievementCreatedConsumer: IConsumer<AchievementCreated>
{
    private readonly ILogger<AchievementCreatedConsumer> _logger;
    private readonly IMediator _mediator;

    public AchievementCreatedConsumer(ILogger<AchievementCreatedConsumer> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<AchievementCreated> context)
    {
        _logger.LogInformation($"Message consumed: Achievement {context.Message.Id} created.");

        await _mediator.Send(new CreateAchievementCommand(context.Message.Id));
    }
}
