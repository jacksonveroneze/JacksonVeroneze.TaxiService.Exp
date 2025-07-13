using TaxiService.Common.BaseDomain.Entities;
using TaxiService.Common.SharedKernel.Utils;

namespace TaxiService.Modules.Passenger.Domain.Entities;

public class OutboxMessage : BaseEntity
{
    public Guid? Id { get; }

    public string? Type { get; }

    public string? Content { get; }

    public DateTime? OccurredOnUtc { get; }

    #region ctor

    protected OutboxMessage()
    {
    }

    private OutboxMessage(
        string? type,
        string? content,
        DateTime? occurredOnUtc)
    {
        ArgumentException.ThrowIfNullOrEmpty(type);
        ArgumentException.ThrowIfNullOrEmpty(content);

        Id = GuidGenerator.Generate();
        Type = type;
        Content = content;
        OccurredOnUtc = occurredOnUtc;
    }

    #endregion

    #region Factory

    public static OutboxMessage Create(
        string? type,
        string? content)
    {
        OutboxMessage entity = new(
            type, content, DateTime.UtcNow);

        return entity;
    }

    #endregion
}
