using JacksonVeroneze.TemplateWebApi.Domain.Core.Primitives;

namespace JacksonVeroneze.TemplateWebApi.Domain.Exceptions;

public class DomainException : Exception
{
    public Error Error { get; } = Error.None;

    private DomainException()
    {
    }

    public DomainException(Error error) : base(error.Message)
    {
        Error = error;
    }
}
