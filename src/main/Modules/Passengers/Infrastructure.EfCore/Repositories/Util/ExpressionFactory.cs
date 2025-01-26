using System.Linq.Expressions;
using TaxiService.Modules.Passengers.Domain.Util;
using TaxiService.Modules.Passengers.Domain.Util.Operators;

namespace TaxiService.Modules.Passengers.Infrastructure.EfCore.Repositories.Util;

public static class ExpressionFactory
{
    public static Expression<Func<T, bool>> Factory<T, TT>(
        Expression<Func<T, TT>> propertyExpression,
        BaseFilter<TT> filter)
    {
        ParameterExpression parameter = propertyExpression.Parameters[0];

        Expression property = propertyExpression.Body;

        Type targetType = Nullable.GetUnderlyingType(typeof(TT)) ?? typeof(TT);

        ConstantExpression constant = Expression.Constant(
            Convert.ChangeType(filter.Value, targetType), targetType);

        Expression comparisonProperty = Nullable.GetUnderlyingType(typeof(TT)) != null
            ? Expression.Convert(property, targetType)
            : property;

        Expression comparison = filter.Operator switch
        {
            FilterOperator.Eq => Expression.Equal(comparisonProperty, constant),
            FilterOperator.Neq => Expression.NotEqual(comparisonProperty, constant),
            FilterOperator.Gt => Expression.GreaterThan(comparisonProperty, constant),
            FilterOperator.Lt => Expression.LessThan(comparisonProperty, constant),
            FilterOperator.Gte => Expression.GreaterThanOrEqual(comparisonProperty, constant),
            FilterOperator.Lte => Expression.LessThanOrEqual(comparisonProperty, constant),
            FilterOperator.StartsWith => ExpressionHelper.StartsWith(comparisonProperty, constant),
            FilterOperator.EndsWith => ExpressionHelper.EndsWith(comparisonProperty, constant),
            FilterOperator.Contains => ExpressionHelper.Contains(comparisonProperty, constant),
            _ => throw new NotSupportedException($"Unsupported filter type: {filter.Operator}")
        };

        return Expression.Lambda<Func<T, bool>>(comparison, parameter);
    }
}