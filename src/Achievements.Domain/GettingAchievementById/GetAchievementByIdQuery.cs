using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;
using Shared.CQRS;

namespace Achievements.Domain.CreatingAchievement;

public class GetAchievementByIdQuery : IQuery<AchievementDto>
{
    public Guid Id { get; }

    public GetAchievementByIdQuery(Guid id) 
    {
        Id = id;
    }
}

public class AchievementDto
{
    public Guid Id { get; init; }
    public bool Unlocked { get; init; }
}

public class GetAchievementByIdCommandHandler : IQueryHandler<GetAchievementByIdQuery, AchievementDto>
{
    private readonly ILogger<GetAchievementByIdCommandHandler> _logger;
    private readonly IConfiguration _configuration;

    public GetAchievementByIdCommandHandler(
        ILogger<GetAchievementByIdCommandHandler> logger,
        IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public async Task<AchievementDto> Handle(GetAchievementByIdQuery query, CancellationToken cancellationToken)
    {
        using IDbConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("postgres"));

        return await connection.QueryFirstOrDefaultAsync<AchievementDto>(
            $"SELECT \"Id\", \"Unlocked\" FROM achievements.\"Achievements\" WHERE \"Id\" = '{query.Id}'");
    }
}
