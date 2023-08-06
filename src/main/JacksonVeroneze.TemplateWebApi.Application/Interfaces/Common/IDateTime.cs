namespace JacksonVeroneze.TemplateWebApi.Application.Interfaces.Common;

public interface IDateTime
{
    DateTime UtcNow { get; }

    DateTime Now { get; }

    DateOnly DateNow { get; }

    TimeOnly TimeNow { get; }
}
