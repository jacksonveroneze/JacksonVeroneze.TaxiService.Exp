using TaxiService.Modules.Passengers.Domain.Util.Operators;

namespace TaxiService.Modules.Passengers.Domain.Util;

public sealed class GuidFilter
    : BaseFilter<Guid>
{
    private const FilterOperator _defaultOperator
        = FilterOperator.Eq;

    private GuidFilter(
        Guid value, FilterOperator op)
    {
        Value = value;
        Operator = op;
    }

    public static GuidFilter Create(
        Guid value, FilterOperator op = _defaultOperator)
        => new(value, op);
}