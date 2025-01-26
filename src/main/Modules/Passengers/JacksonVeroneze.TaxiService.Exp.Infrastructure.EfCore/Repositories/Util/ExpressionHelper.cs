using System.Linq.Expressions;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.EfCore.Repositories.Util;

public static class ExpressionHelper
{
    public static Expression StartsWith
        (Expression property, Expression value)
    {
        return Expression.Call(property,
            nameof(string.StartsWith), Type.EmptyTypes, value);
    }

    public static Expression EndsWith(
        Expression property, Expression value)
    {
        return Expression.Call(property,
            nameof(string.EndsWith), Type.EmptyTypes, value);
    }

    public static Expression Contains(
        Expression property, Expression value)
    {
        return Expression.Call(property,
            nameof(string.Contains), Type.EmptyTypes, value);
    }
}