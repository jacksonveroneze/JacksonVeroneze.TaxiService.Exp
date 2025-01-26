using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace TaxiService.Modules.Rides.Domain.Specifications.Base.Predicate.Util;

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