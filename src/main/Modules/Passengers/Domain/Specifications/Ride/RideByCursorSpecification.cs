using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using TaxiService.Modules.Passengers.Domain.Entities;
using TaxiService.Modules.Passengers.Domain.Specifications.Base;

namespace TaxiService.Modules.Passengers.Domain.Specifications.Ride;

[ExcludeFromCodeCoverage]
public class RideByCursorSpecification(Guid? userId)
    : BaseSpecification<RideEntity>
{
    public override Expression<Func<RideEntity, bool>> ToExpression()
    {
        return userId.HasValue ? spec => spec.UserId > userId : DefaultExpression;
    }
}