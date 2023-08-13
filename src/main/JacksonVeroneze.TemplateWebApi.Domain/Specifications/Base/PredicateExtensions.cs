using System.Linq.Expressions;

namespace JacksonVeroneze.TemplateWebApi.Domain.Specifications.Base;

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

internal class SubstExpressionVisitor : ExpressionVisitor
{
    public readonly Dictionary<Expression, Expression> Subst = new();

    protected override Expression VisitParameter(
        ParameterExpression node)
    {
        return Subst.TryGetValue(node, out Expression? newValue)
            ? newValue
            : node;
    }
}
