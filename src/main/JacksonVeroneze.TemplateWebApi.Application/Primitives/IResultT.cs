namespace JacksonVeroneze.TemplateWebApi.Application.Primitives;

public interface IResult<out TValue> : IResult
{
    public TValue? Value { get; }
}
