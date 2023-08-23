namespace JacksonVeroneze.TemplateWebApi.Domain.Core.Primitives;

public class ValidationError
{
    public string? Identifier { get; init; }

    public string? ErrorMessage { get; init; }

    public string? ErrorCode { get; init; }
}
