using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using TaxiService.Modules.Rides.Domain.Entities;
using TaxiService.Common.Domain.Specifications.Base;

namespace TaxiService.Modules.Rides.Domain.Specifications.Ride;

[ExcludeFromCodeCoverage]
public class RideByCursorSpecification(Guid? userId)
    : BaseSpecification<RideEntity>
{
    public override Expression<Func<RideEntity, bool>> ToExpression()
    {
        return userId.HasValue ? spec => spec.UserId > userId : DefaultExpression;
    }
}