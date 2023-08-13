using JacksonVeroneze.TemplateWebApi.Domain.Core.Primitives;

namespace JacksonVeroneze.TemplateWebApi.Application.Primitives;

public interface IResult
{
    public ResultStatus Status { get; }

    public Error? Error { get; set; }

    public IList<ValidationError>? ValidationErrors { get; }
}
