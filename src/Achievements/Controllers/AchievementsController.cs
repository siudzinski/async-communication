using Achievements.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<ActionResult> Create()
    {
        await _mediator.Send(new CreateAchievementCommand());

        return Ok();
    }

    [HttpPost("achievements/{achievementId}/unlock")]
    public async Task<ActionResult> Unlock(Guid achievementId)
    {
        await _mediator.Send(new UnlockAchievementCommand(achievementId));

        return Ok();
    }

    // [HttpPost("achievements/{achievementId}/accomplish")]
    // public async Task<ActionResult> Accomplish(Guid achievementId)
    // {
    //     await _publishEndpoint.Publish<IAchievementCompleted>(new { Id = achievementId });

    //     return Ok();
    // }
}
