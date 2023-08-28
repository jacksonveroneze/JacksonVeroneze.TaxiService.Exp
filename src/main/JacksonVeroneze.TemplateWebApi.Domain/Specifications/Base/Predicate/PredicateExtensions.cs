using System.Linq.Expressions;
using JacksonVeroneze.TemplateWebApi.Domain.Specifications.Base.Predicate.Util;

namespace JacksonVeroneze.TemplateWebApi.Domain.Specifications.Base.Predicate;

public static class PredicateExtensions
{
    public static Expression<Func<TType, bool>> And<TType>(
        this Expression<Func<TType, bool>> a,
        Expression<Func<TType, bool>> b)
    {
        ArgumentNullException.ThrowIfNull(a);
        ArgumentNullException.ThrowIfNull(b);

        ParameterExpression parameter = a.Parameters[0];

        SubstExpressionVisitor visitor =
            new() { Subst = { [b.Parameters[0]] = parameter } };

        BinaryExpression body = Expression
            .AndAlso(a.Body, visitor.Visit(b.Body));

        return Expression
            .Lambda<Func<TType, bool>>(body, parameter);
    }

    public static Expression<Func<TType, bool>> Or<TType>(
        this Expression<Func<TType, bool>> a,
        Expression<Func<TType, bool>> b)
    {
        ArgumentNullException.ThrowIfNull(a);
        ArgumentNullException.ThrowIfNull(b);

        ParameterExpression parameter = a.Parameters[0];

        SubstExpressionVisitor visitor =
            new() { Subst = { [b.Parameters[0]] = parameter } };

        BinaryExpression body = Expression
            .Or(a.Body, visitor.Visit(b.Body));

        return Expression
            .Lambda<Func<TType, bool>>(body, parameter);
    }
}
