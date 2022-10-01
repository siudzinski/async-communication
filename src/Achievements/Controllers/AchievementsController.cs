using Achievements.Contracts;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Achievements.Controllers;

[ApiController]
public class AchievementsController : ControllerBase
{
    private readonly ILogger<AchievementsController> _logger;
    private readonly IPublishEndpoint _publishEndpoint;

    public AchievementsController(
        ILogger<AchievementsController> logger, 
        IPublishEndpoint publishEndpoint)
    {
        _logger = logger;
        _publishEndpoint = publishEndpoint;
    }

    [HttpPost("achievements/unlock/{achievementId}")]
    public async Task<ActionResult> Unlock(Guid achievementId)
    {
        await _publishEndpoint.Publish<IAchievementUnlocked>(new { Id = achievementId });

        return Ok();
    }
}
