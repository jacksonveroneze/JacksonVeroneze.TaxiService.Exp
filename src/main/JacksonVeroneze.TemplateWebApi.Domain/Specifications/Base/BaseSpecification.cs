using System.Linq.Expressions;
using NetDevPack.Specification;

namespace JacksonVeroneze.TemplateWebApi.Domain.Specifications.Base;

// public abstract class BaseSpecification<TEntity> where TEntity : class
// {
//     protected abstract Expression<Func<TEntity, bool>> ToExpression();
//
//     protected abstract Func<TEntity, bool> ToFunc();
//
//     internal bool IsSatisfiedBy(TEntity entity)
//     {
//         return ToExpression().Compile()(entity);
//     }
//
//     public static implicit operator Expression<Func<TEntity, bool>>(BaseSpecification<TEntity> specification)
//     {
//         return specification.ToExpression();
//     }
//
//     public static implicit operator Func<TEntity, bool>(BaseSpecification<TEntity> specification)
//     {
//         return specification.ToFunc();
//     }
// }

public abstract class BaseSpecification<TEntity> : Specification<TEntity> where TEntity : class
{
    protected abstract Func<TEntity, bool> ToFunc();

    public static implicit operator Expression<Func<TEntity, bool>>(BaseSpecification<TEntity> specification)
    {
        return specification.ToExpression();
    }

    public static implicit operator Func<TEntity, bool>(BaseSpecification<TEntity> specification)
    {
        return specification.ToFunc();
    }
}
