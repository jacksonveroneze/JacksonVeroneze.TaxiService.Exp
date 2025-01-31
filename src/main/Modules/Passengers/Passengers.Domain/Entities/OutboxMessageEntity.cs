using TaxiService.Common.BaseDomain.Entities.Base;

namespace TaxiService.Modules.Passengers.Domain.Entities;

public class OutboxMessageEntity : BaseEntityAggregateRoot
{
    public Guid? Id { get; }

    public string? Type { get; }

    public string? Content { get; }

    public DateTime? OccurredOnUtc { get; }

    #region ctor

    public OutboxMessageEntity()
    {
    }

    public OutboxMessageEntity(
        string? type,
        string? content,
        DateTime? occurredOnUtc)
    {
        ArgumentException.ThrowIfNullOrEmpty(type);
        ArgumentException.ThrowIfNullOrEmpty(content);

        Id = Guid.NewGuid();
        Type = type;
        Content = content;
        OccurredOnUtc = occurredOnUtc;
    }

    #endregion

    #region Factory

    // Factory Method
    public static OutboxMessageEntity Create(
        string? type,
        string? content)
    {
        OutboxMessageEntity entity = new(
            type, content, DateTime.UtcNow);

        return entity;
    }

    #endregion
}