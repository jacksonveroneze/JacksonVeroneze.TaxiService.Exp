using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace JacksonVeroneze.TaxiService.Exp.Domain.Specifications.Base.Predicate.Util;

[ExcludeFromCodeCoverage]
public class SubstExpressionVisitor : ExpressionVisitor
{
    public Dictionary<Expression, Expression> Subst { get; } = [];

    protected override Expression VisitParameter(
        ParameterExpression node)
    {
        return Subst.GetValueOrDefault(node, node);
    }
}