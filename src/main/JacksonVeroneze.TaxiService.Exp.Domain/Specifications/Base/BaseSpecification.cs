using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace JacksonVeroneze.TaxiService.Exp.Domain.Specifications.Base;

[ExcludeFromCodeCoverage]
public abstract class BaseSpecification<TEntity> where TEntity : class
{
    protected Expression<Func<TEntity, bool>> DefaultExpression => _ => true;

    public abstract Expression<Func<TEntity, bool>> ToExpression();

    public static implicit operator Expression<Func<TEntity, bool>>(
        BaseSpecification<TEntity> specification)
    {
        ArgumentNullException.ThrowIfNull(specification);

        return specification.ToExpression();
    }
}