using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using TaxiService.Modules.Rides.Domain.Entities;
using TaxiService.Modules.Rides.Domain.Specifications.Base;

namespace TaxiService.Modules.Rides.Domain.Specifications.Ride;

[ExcludeFromCodeCoverage]
public class RideByIdSpecification(Guid id)
    : BaseSpecification<RideEntity>
{
    public override Expression<Func<RideEntity, bool>> ToExpression()
    {
        return spec => spec.Id == id;
    }
}