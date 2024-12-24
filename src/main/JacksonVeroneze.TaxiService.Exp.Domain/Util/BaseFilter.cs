using JacksonVeroneze.TaxiService.Exp.Domain.Util.Operators;

namespace JacksonVeroneze.TaxiService.Exp.Domain.Util;

public abstract class BaseFilter<TType>
{
    public TType? Value { get; protected init; }

    public FilterOperator? Operator { get; protected init; }
}
