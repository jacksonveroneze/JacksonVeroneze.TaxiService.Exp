using TaxiService.Modules.Passengers.Domain.Util.Operators;

namespace TaxiService.Modules.Passengers.Domain.Util;

public sealed class DateOnlyFilter
    : BaseFilter<DateOnly?>
{
    private const FilterOperator _defaultOperator
        = FilterOperator.Eq;

    private DateOnlyFilter(
        DateOnly? value, FilterOperator op)
    {
        Value = value;
        Operator = op;
    }

    public static DateOnlyFilter Create(
        DateOnly? value, FilterOperator op = _defaultOperator)
        => new(value, op);
}