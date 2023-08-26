using JacksonVeroneze.NET.DomainObjects;

namespace JacksonVeroneze.TemplateWebApi.Domain.Core.Primitives;

public interface IResult
{
    public ResultStatus Status { get; }

    public Error? Error { get; set; }

    public IList<ValidationError>? ValidationErrors { get; }

    public bool IsSuccess { get; }

    public bool IsFailure { get; }
}
