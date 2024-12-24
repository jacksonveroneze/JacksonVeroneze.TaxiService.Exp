namespace JacksonVeroneze.TaxiService.Exp.Domain.Util;

public abstract class BaseFilter<TType, TOperatorType>
{
    public TType? Value { get; protected init; }

    public TOperatorType? Op { get; protected init; }
}