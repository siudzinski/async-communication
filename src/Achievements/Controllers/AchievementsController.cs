using MediatR;
using Microsoft.AspNetCore.Mvc;
using Achievements.Domain.CreatingAchievement;
using Achievements.Domain.UnlockingAchievement;

namespace Achievements.Controllers;

[ApiController]
public class AchievementsController : ControllerBase
{
    private readonly ILogger<AchievementsController> _logger;
    private readonly IMediator _mediator;


    public AchievementsController(
        ILogger<AchievementsController> logger,
        IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost("achievements")]
    public async Task<ActionResult> Create(CancellationToken cancellationToken)
    {
        var command = new CreateAchievementCommand();

        await _mediator.Send(new CreateAchievementCommand(), cancellationToken);

        return Created($"achievements/{command.Id}", command.Id);
    }

    [HttpGet("achievements/{id}")]
    public async Task<ActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAchievementByIdQuery(id), cancellationToken);

        return Ok(result);
    }

    [HttpPost("achievements/{achievementId}/unlock")]
    public async Task<ActionResult> Unlock(Guid achievementId, CancellationToken cancellationToken)
    {
        await _mediator.Send(new UnlockAchievementCommand(achievementId), cancellationToken);

        return Ok();
    }
}
