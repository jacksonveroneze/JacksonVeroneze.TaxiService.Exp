using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using TaxiService.Common.BaseDomain.Specifications.Base;
using TaxiService.Modules.Rides.Domain.Entities;

namespace TaxiService.Modules.Rides.Domain.Specifications.Ride;

[ExcludeFromCodeCoverage]
public class RideByCursorSpecification(Guid? passengerId)
    : BaseSpecification<RideEntity>
{
    public override Expression<Func<RideEntity, bool>> ToExpression()
    {
        return passengerId.HasValue ? spec => spec.PassengerId > passengerId : DefaultExpression;
    }
}
