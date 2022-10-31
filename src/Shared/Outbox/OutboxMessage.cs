using System.Text.Json;

namespace Shared.Outbox;

public class OutboxMessage
{
    public Guid Id { get; private set; }
    public DateTime OccurredOn { get; private set; }
    public DateTime? ProcessedDate { get; private set; }
    public string Type { get; private set; }
    public string Data { get; private set; }

    private OutboxMessage() { }

    private OutboxMessage(DateTime occurredOn, string type, string data)
    {
        Id = Guid.NewGuid();
        OccurredOn = occurredOn;
        Type = type;
        Data = data;
        ProcessedDate = null;
    }

    public static OutboxMessage Create(object data)
    {
        return new OutboxMessage(
            DateTime.UtcNow, 
            data.GetType().FullName,
            JsonSerializer.Serialize(data));
    }
}
