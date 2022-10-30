using System.Data;
using System.Reflection;
using System.Text.Json;
using Dapper;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Shared.Outbox;

namespace Achievements.Infrastructure;

public class OutboxBackgroundService : BackgroundService
{
    private readonly ILogger<OutboxBackgroundService> _logger;
    private readonly IServiceScopeFactory _scopeFactory;

    private readonly PeriodicTimer _timer = new(TimeSpan.FromSeconds(20));

    public OutboxBackgroundService(
        ILogger<OutboxBackgroundService> logger,
        IServiceScopeFactory scopeFactory)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested && await _timer.WaitForNextTickAsync(cancellationToken))
        {
            await ProcessOutboxMessages(cancellationToken);
        }
    }

    private async Task ProcessOutboxMessages(CancellationToken cancellationToken)
    {
        using var scope = _scopeFactory.CreateScope();
        var publishEndpoint = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();
        var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

        using IDbConnection connection = new NpgsqlConnection(configuration.GetConnectionString("postgres"));

        var outboxMessages = await GetOutboxMessagesToProcess(connection);
        foreach(var outboxMessage in outboxMessages)
        {
            var message = DeserializeMessage(outboxMessage.Data, outboxMessage.Type);

            await publishEndpoint.Publish(message, cancellationToken);

            await ExecuteProcessedDateUpdate(connection, outboxMessage.Id);
        }
    }

    private object DeserializeMessage(string data, string type)
    {
        var assembly = Assembly.GetAssembly(typeof(Contracts.AchievementUnlocked));
        var messageType = assembly.GetType(type);

        return JsonSerializer.Deserialize(data, messageType);
    }

    private async Task<IEnumerable<OutboxMessage>> GetOutboxMessagesToProcess(IDbConnection connection) =>
        await connection.QueryAsync<OutboxMessage>("SELECT * FROM achievements.\"OutboxMessages\" WHERE \"ProcessedDate\" IS NULL");

    private async Task ExecuteProcessedDateUpdate(IDbConnection connection, Guid outboxMessageId) =>
        await connection.ExecuteAsync(
            "UPDATE achievements.\"OutboxMessages\" SET \"ProcessedDate\" = @ProcessedDate WHERE \"Id\" = @Id", 
            new { ProcessedDate = DateTime.UtcNow, Id = outboxMessageId });
}
