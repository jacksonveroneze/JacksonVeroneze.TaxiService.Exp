using JacksonVeroneze.TaxiService.Exp.Domain.Util.Operators;

namespace JacksonVeroneze.TaxiService.Exp.Domain.Util;

public sealed class DateOnlyFilter
    : BaseFilter<DateOnly?, DateOnlyOperator>
{
    private const DateOnlyOperator _defaultOperator
        = DateOnlyOperator.Equals;

    private DateOnlyFilter(
        DateOnly? value, DateOnlyOperator op)
    {
        Value = value;
        Op = op;
    }

    public static DateOnlyFilter Create(
        DateOnly? value, DateOnlyOperator op = _defaultOperator)
        => new(value, op);
}