using TaxiService.Modules.Rides.Domain.Util.Operators;

namespace TaxiService.Modules.Rides.Domain.Util;

public sealed class StringFilter
    : BaseFilter<string>
{
    private const FilterOperator _defaultOperator
        = FilterOperator.Eq;

    private StringFilter(
        string? value, FilterOperator op)
    {
        Value = value;
        Operator = op;
    }

    public static StringFilter Create(
        string? value, FilterOperator op = _defaultOperator)
        => new(value, op);
}