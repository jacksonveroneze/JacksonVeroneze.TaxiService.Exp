using JacksonVeroneze.TaxiService.Exp.Domain.Util.Operators;

namespace JacksonVeroneze.TaxiService.Exp.Domain.Util;

public sealed class GuidFilter
    : BaseFilter<Guid, GuidOperator>
{
    private const GuidOperator _defaultOperator
        = GuidOperator.Equals;

    private GuidFilter(
        Guid value, GuidOperator op)
    {
        Value = value;
        Op = op;
    }

    public static GuidFilter Create(
        Guid value, GuidOperator op = _defaultOperator)
        => new(value, op);
}