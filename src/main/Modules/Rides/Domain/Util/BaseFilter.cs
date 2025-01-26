using TaxiService.Modules.Rides.Domain.Util.Operators;

namespace TaxiService.Modules.Rides.Domain.Util;

public abstract class BaseFilter<TType>
{
    public TType? Value { get; protected init; }

    public FilterOperator? Operator { get; protected init; }
}
