namespace JacksonVeroneze.TemplateWebApi.Domain.Core.Primitives;

public sealed class Error
{
    public string Code { get; }

    public string Message { get; }

    public static implicit operator string(Error error) => error?.Code ?? string.Empty;

    public Error(string code, string message)
    {
        Code = code;
        Message = message;
    }
}
