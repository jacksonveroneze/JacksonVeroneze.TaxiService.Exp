using System.Linq.Expressions;
using JacksonVeroneze.TaxiService.Exp.Domain.Util;
using LinqKit;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.EfCore.Repositories.Util;

public class ExpressionBuilder<T>
{
    private readonly ExpressionStarter<T> _predicate =
        PredicateBuilder.New<T>(true);

    public ExpressionBuilder<T> AddFilter(
        Expression<Func<T, Guid>> propertyExpression,
        BaseFilter<Guid>? filter)
    {
        if (filter?.Value is null)
        {
            return this;
        }

        Expression<Func<T, bool>> expression =
            ExpressionsBuilders.BuildExpression(
                propertyExpression, filter);

        _predicate.And(expression);

        return this;
    }

    public ExpressionBuilder<T> AddFilter(
        Expression<Func<T, string>> propertyExpression,
        BaseFilter<string>? filter)
    {
        if (filter?.Value is null)
        {
            return this;
        }

        Expression<Func<T, bool>> expression =
            ExpressionsBuilders.BuildExpression(
                propertyExpression, filter);

        _predicate.And(expression);

        return this;
    }

    public ExpressionBuilder<T> AddFilter(
        Expression<Func<T, DateOnly>> propertyExpression,
        BaseFilter<DateOnly>? filter)
    {
        if (filter?.Value is null)
        {
            return this;
        }

        Expression<Func<T, bool>> expression =
            ExpressionsBuilders.BuildExpression(
                propertyExpression, filter);

        _predicate.And(expression);

        return this;
    }

    public ExpressionBuilder<T> AddFilter<TEnum>(
        Expression<Func<T, TEnum>> propertyExpression,
        BaseFilter<TEnum> filter)
    {
        if (filter.Value is null)
        {
            return this;
        }

        Expression<Func<T, bool>> expression =
            ExpressionsBuilders.BuildExpression(
                propertyExpression, filter);

        _predicate.And(expression);

        return this;
    }

    public ExpressionStarter<T> Build()
    {
        return _predicate;
    }
}
