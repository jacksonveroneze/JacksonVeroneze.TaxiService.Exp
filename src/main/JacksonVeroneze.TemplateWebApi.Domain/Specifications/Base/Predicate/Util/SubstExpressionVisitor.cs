using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace JacksonVeroneze.TemplateWebApi.Domain.Specifications.Base.Predicate.Util;

[ExcludeFromCodeCoverage]
public class SubstExpressionVisitor : ExpressionVisitor
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
