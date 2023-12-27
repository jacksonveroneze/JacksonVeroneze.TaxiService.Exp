using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace JacksonVeroneze.TaxiService.Exp.Domain.Specifications.Base.Predicate.Util;

[ExcludeFromCodeCoverage]
public class SubstExpressionVisitor : ExpressionVisitor
{
    public readonly Dictionary<Expression, Expression> Subst = [];

    protected override Expression VisitParameter(
        ParameterExpression node)
    {
        return Subst.GetValueOrDefault(node, node);
    }
}
