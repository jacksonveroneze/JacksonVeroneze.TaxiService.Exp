using System.Linq.Expressions;
using LinqKit;
using TaxiService.Modules.Passengers.Domain.Util;

namespace TaxiService.Modules.Passengers.Infrastructure.EfCore.Repositories.Util;

public class ExpressionsBuilder<T>
{
    private readonly ExpressionStarter<T> _predicate =
        PredicateBuilder.New<T>(true);

    public ExpressionsBuilder<T> AddFilter<TProperty>(
        Expression<Func<T, TProperty>> propertyExpression,
        BaseFilter<TProperty>? filter)
    {
        if (filter is null || filter.Value is null)
        {
            return this;
        }

        Expression<Func<T, bool>> expression =
            ExpressionFactory.Factory(propertyExpression, filter);

        _predicate.And(expression);

        return this;
    }

    public ExpressionStarter<T> Build()
    {
        return _predicate;
    }
}