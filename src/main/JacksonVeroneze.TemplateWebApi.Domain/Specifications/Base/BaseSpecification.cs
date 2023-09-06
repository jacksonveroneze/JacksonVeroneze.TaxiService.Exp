using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace JacksonVeroneze.TemplateWebApi.Domain.Specifications.Base;

[ExcludeFromCodeCoverage]
public abstract class BaseSpecification<TEntity> where TEntity : class
{
    public abstract Expression<Func<TEntity, bool>> ToExpression();

    public static implicit operator Expression<Func<TEntity, bool>>(
        BaseSpecification<TEntity> specification)
    {
        ArgumentNullException.ThrowIfNull(specification);

        return specification.ToExpression();
    }

    public bool IsSatisfiedBy(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        return ToExpression().Compile()(entity);
    }
}
