using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using TaxiService.Modules.Passengers.Domain.Entities;
using TaxiService.Modules.Passengers.Domain.Specifications.Base;

namespace TaxiService.Modules.Passengers.Domain.Specifications.Ride;

[ExcludeFromCodeCoverage]
public class RideByIdSpecification(Guid id)
    : BaseSpecification<RideEntity>
{
    public override Expression<Func<RideEntity, bool>> ToExpression()
    {
        return spec => spec.Id == id;
    }
}