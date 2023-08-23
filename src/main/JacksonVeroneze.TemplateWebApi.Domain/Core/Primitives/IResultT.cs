namespace JacksonVeroneze.TemplateWebApi.Domain.Core.Primitives;

public interface IResult<out TValue> : IResult
{
    public TValue? Value { get; }
}
