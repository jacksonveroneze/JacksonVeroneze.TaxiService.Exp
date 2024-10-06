using JacksonVeroneze.TaxiService.Exp.Domain.Entities.Base;

namespace JacksonVeroneze.TaxiService.Exp.Domain.Entities;

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
        ArgumentNullException.ThrowIfNull(occurredOnUtc);

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