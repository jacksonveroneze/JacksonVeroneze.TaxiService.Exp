using JacksonVeroneze.TaxiService.Exp.Domain.Util.Operators;

namespace JacksonVeroneze.TaxiService.Exp.Domain.Util;

public sealed class EnumFilter<TEnum>
    : BaseFilter<TEnum?>
    where TEnum : struct, Enum
{
    private const FilterOperator _defaultOperator
        = FilterOperator.Eq;

    private EnumFilter(
        TEnum? value, FilterOperator op)
    {
        Value = value;
        Operator = op;
    }

    public static EnumFilter<TEnum> Create(
        TEnum? value, FilterOperator op = _defaultOperator)
        => new(value, op);
}