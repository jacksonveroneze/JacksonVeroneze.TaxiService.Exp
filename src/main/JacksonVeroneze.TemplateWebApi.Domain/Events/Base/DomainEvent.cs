namespace JacksonVeroneze.TemplateWebApi.Domain.Events.Base;

public abstract class DomainEvent
{
    public DateTimeOffset DateOccurred { get; private set; }
        = DateTimeOffset.UtcNow;
}
