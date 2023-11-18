using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Specifications.Base;

namespace JacksonVeroneze.TemplateWebApi.Domain.Specifications.Ride;

[ExcludeFromCodeCoverage]
public class RideUserIdSpecification : BaseSpecification<RideEntity>
{
    private readonly Expression<Func<RideEntity, bool>> _defaultExpression = _ => true;

    private readonly Guid? _userId;

    public RideUserIdSpecification(Guid? userId)
    {
        _userId = userId;
    }


    public override Expression<Func<RideEntity, bool>> ToExpression()
    {
        if (!_userId.HasValue)
        {
            return _defaultExpression;
        }

        return spec => spec.User!.Id == _userId;
    }
}
