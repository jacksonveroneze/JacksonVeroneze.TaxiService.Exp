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
        GuidFilter? filter)
    {
        if (filter?.Value is null)
        {
            return this;
        }

        Expression<Func<T, bool>> expression =
            ExpressionsBuilders.BuildGuidExpression(
                propertyExpression, filter);

        _predicate.And(expression);

        return this;
    }

    public ExpressionBuilder<T> AddFilter(
        Expression<Func<T, string>> propertyExpression,
        StringFilter? filter)
    {
        if (filter?.Value is null)
        {
            return this;
        }

        Expression<Func<T, bool>> expression =
            ExpressionsBuilders.BuildStringExpression(
                propertyExpression, filter);

        _predicate.And(expression);

        return this;
    }

    public ExpressionBuilder<T> AddFilter(
        Expression<Func<T, DateOnly>> propertyExpression,
        DateOnlyFilter? filter)
    {
        if (filter?.Value is null)
        {
            return this;
        }

        Expression<Func<T, bool>> expression =
            ExpressionsBuilders.BuildDateOnlyExpression(
                propertyExpression, filter);

        _predicate.And(expression);

        return this;
    }

    public ExpressionBuilder<T> AddFilter<TEnum>(
        Expression<Func<T, TEnum>> propertyExpression,
        EnumFilter<TEnum>? filter) where TEnum : struct, Enum
    {
        if (filter?.Value is null)
        {
            return this;
        }

        Expression<Func<T, bool>> expression =
            ExpressionsBuilders.BuildEnumExpression(
                propertyExpression, filter);

        _predicate.And(expression);

        return this;
    }

    public ExpressionStarter<T> Build()
    {
        return _predicate;
    }
}