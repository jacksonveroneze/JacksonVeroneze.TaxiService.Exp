using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using JacksonVeroneze.TaxiService.Exp.Domain.Specifications.Base;

namespace JacksonVeroneze.TaxiService.Exp.Domain.Specifications.Ride;

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

        return spec => spec.UserId == _userId;
    }
}
