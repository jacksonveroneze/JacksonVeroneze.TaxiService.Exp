using System.Linq.Expressions;
using JacksonVeroneze.TaxiService.Exp.Domain.Filters;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.EfCore.Repositories.Util;

public static class ExpressionsBuilders
{
    public static Expression<Func<T, bool>> BuildGuidExpression<T>(
        Expression<Func<T, Guid>> property,
        GuidFilter filter)
    {
        ParameterExpression parameter = property.Parameters[0];
        Expression propertyAccess = property.Body;
        ConstantExpression constant = Expression.Constant(filter.Value);

        Expression comparison = filter.Op switch
        {
            GuidOperator.Equals => Expression.Equal(propertyAccess, constant),
            GuidOperator.NotEquals => Expression.NotEqual(propertyAccess, constant),
            _ => throw new NotImplementedException()
        };

        return Expression.Lambda<Func<T, bool>>(comparison, parameter);
    }

    public static Expression<Func<T, bool>> BuildStringExpression<T>(
        Expression<Func<T, string>> property,
        StringFilter filter)
    {
        ParameterExpression parameter = property.Parameters[0];
        Expression propertyAccess = property.Body;
        ConstantExpression constant = Expression.Constant(filter.Value);

        Expression comparison = filter.Op switch
        {
            StringOperator.StartsWith => Expression.Call(propertyAccess, nameof(string.StartsWith),
                null, constant),
            StringOperator.EndsWith => Expression.Call(propertyAccess, nameof(string.EndsWith),
                null, constant),
            StringOperator.Contains => Expression.Call(propertyAccess, nameof(string.Contains),
                null, constant),
            StringOperator.Equals => Expression.Equal(propertyAccess, constant),
            _ => throw new NotImplementedException()
        };

        return Expression.Lambda<Func<T, bool>>(comparison, parameter);
    }

    public static Expression<Func<T, bool>> BuildEnumExpression<T, TEnum>(
        Expression<Func<T, TEnum>> property,
        EnumFilter<TEnum> filter) where TEnum : struct, Enum
    {
        ParameterExpression parameter = property.Parameters[0];
        Expression propertyAccess = property.Body;
        ConstantExpression constant = Expression.Constant(filter.Value, typeof(TEnum));

        Expression comparison = filter.Operator switch
        {
            EnumOperator.Equals => Expression.Equal(propertyAccess, constant),
            EnumOperator.NotEquals => Expression.NotEqual(propertyAccess, constant),
            _ => throw new NotImplementedException()
        };

        return Expression.Lambda<Func<T, bool>>(comparison, parameter);
    }

    // public Expression<Func<T, bool>> BuildNumericExpression<T>(
    //     Expression<Func<T, int>> property,
    //     NumericFilter filter)
    // {
    //     ParameterExpression parameter = property.Parameters[0];
    //     Expression propertyAccess = property.Body;
    //     ConstantExpression constant = Expression.Constant(filter.Value!.Value);
    //
    //     Expression comparison = filter.Operator switch
    //     {
    //         NumericOperator.GreaterThan => Expression.GreaterThan(propertyAccess, constant),
    //         NumericOperator.LessThan => Expression.LessThan(propertyAccess, constant),
    //         NumericOperator.Equals => Expression.Equal(propertyAccess, constant),
    //         _ => throw new NotImplementedException()
    //     };
    //
    //     return Expression.Lambda<Func<T, bool>>(comparison, parameter);
    // }
}