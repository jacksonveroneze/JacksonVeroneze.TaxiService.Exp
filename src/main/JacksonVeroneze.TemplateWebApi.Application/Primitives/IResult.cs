using JacksonVeroneze.NET.DomainObjects;

namespace JacksonVeroneze.TemplateWebApi.Application.Primitives;

public interface IResult
{
    public ResultStatus Status { get; }

    public Error? Error { get; set; }

    public IList<ValidationError>? ValidationErrors { get; }
}
