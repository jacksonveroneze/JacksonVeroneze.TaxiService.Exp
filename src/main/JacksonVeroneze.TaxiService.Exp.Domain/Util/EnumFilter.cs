using JacksonVeroneze.TaxiService.Exp.Domain.Util.Operators;

namespace JacksonVeroneze.TaxiService.Exp.Domain.Util;

public sealed class EnumFilter<TEnum>
    : BaseFilter<TEnum?, EnumOperator>
    where TEnum : struct, Enum
{
    private const EnumOperator _defaultOperator
        = EnumOperator.Equals;

    private EnumFilter(
        TEnum? value, EnumOperator op)
    {
        Value = value;
        Op = op;
    }

    public static EnumFilter<TEnum> Create(
        TEnum? value, EnumOperator op = _defaultOperator)
        => new(value, op);
}