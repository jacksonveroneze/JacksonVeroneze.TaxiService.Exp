using JacksonVeroneze.TaxiService.Exp.Domain.Util.Operators;

namespace JacksonVeroneze.TaxiService.Exp.Domain.Util;

public sealed class StringFilter
    : BaseFilter<string, StringOperator>
{
    private const StringOperator _defaultOperator
        = StringOperator.Equals;

    private StringFilter(
        string? value, StringOperator op)
    {
        Value = value;
        Op = op;
    }

    public static StringFilter Create(
        string? value, StringOperator op = _defaultOperator)
        => new(value, op);
}