using System.Linq.Expressions;

namespace JacksonVeroneze.TemplateWebApi.Domain.Specifications.Base;

public abstract class BaseSpecification<TEntity> where TEntity : class
{
    public abstract Expression<Func<TEntity, bool>> ToExpression();

    protected abstract Func<TEntity, bool> ToFunc();

    public static implicit operator Expression<Func<TEntity, bool>>(
        BaseSpecification<TEntity> specification)
    {
        return specification.ToExpression();
    }

    public static implicit operator Func<TEntity, bool>(
        BaseSpecification<TEntity> specification)
    {
        return specification.ToFunc();
    }
}
