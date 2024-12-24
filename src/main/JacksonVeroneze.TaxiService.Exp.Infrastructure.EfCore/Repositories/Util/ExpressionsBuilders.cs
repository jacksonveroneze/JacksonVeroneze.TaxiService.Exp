using System.Linq.Expressions;
using JacksonVeroneze.TaxiService.Exp.Domain.Util;
using JacksonVeroneze.TaxiService.Exp.Domain.Util.Operators;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.EfCore.Repositories.Util;

public static class ExpressionsBuilders
{
    public static Expression<Func<T, bool>> BuildExpression<T, TProperty>(
        Expression<Func<T, TProperty>> property,
        BaseFilter<TProperty> filter)
    {
        ParameterExpression parameter = property.Parameters[0];
        Expression propertyAccess = property.Body;
        ConstantExpression constant = Expression.Constant(filter.Value);

        bool isNullable = Nullable.GetUnderlyingType(typeof(TProperty)) != null;

        Expression comparison = filter.Operator switch
        {
            FilterOperator.Eq => isNullable
                ? BuildNullableEquality(propertyAccess, constant, true)
                : Expression.Equal(propertyAccess, constant),

            FilterOperator.NotEq => isNullable
                ? BuildNullableEquality(propertyAccess, constant, false)
                : Expression.NotEqual(propertyAccess, constant),
            FilterOperator.Lt => Expression.LessThan(propertyAccess, constant),
            FilterOperator.Lte => Expression.LessThanOrEqual(propertyAccess, constant),
            FilterOperator.Gt => Expression.GreaterThan(propertyAccess, constant),
            FilterOperator.Gte => Expression.GreaterThanOrEqual(propertyAccess, constant),
            FilterOperator.StartsWith => Expression.Call(propertyAccess, nameof(string.StartsWith), null, constant),
            FilterOperator.EndsWith => Expression.Call(propertyAccess, nameof(string.EndsWith), null, constant),
            FilterOperator.Contains => Expression.Call(propertyAccess, nameof(string.Contains), null, constant),

            _ => throw new NotImplementedException()
        };

        return Expression.Lambda<Func<T, bool>>(comparison, parameter);
    }

    private static Expression BuildNullableEquality(
        Expression propertyAccess,
        ConstantExpression constant,
        bool isEqual)
    {
        // Verifica se a propriedade (HasValue) e compara usando seu valor (Value)
        Expression hasValue = Expression.Property(propertyAccess, "HasValue");
        Expression valueAccess = Expression.Property(propertyAccess, "Value");

        Expression equalityComparison = isEqual
            ? Expression.Equal(valueAccess, constant)
            : Expression.NotEqual(valueAccess, constant);

        // Combina as condições: HasValue && (Value == Constant)
        return Expression.AndAlso(hasValue, equalityComparison);
    }
}