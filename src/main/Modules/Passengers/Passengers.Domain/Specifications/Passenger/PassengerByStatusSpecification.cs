using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

using TaxiService.Common.BaseDomain.Specifications.Base;
using TaxiService.Modules.Passengers.Domain.Entities;
using TaxiService.Modules.Passengers.Domain.Enums;

namespace TaxiService.Modules.Passengers.Domain.Specifications.Passenger;

[ExcludeFromCodeCoverage]
public class PassengerByStatusSpecification(PassengerStatus? status) :
    BaseSpecification<PassengerEntity>
{
    public override Expression<Func<PassengerEntity, bool>> ToExpression()
    {
        return status.HasValue ? spec => spec.Status == status : DefaultExpression;
    }
}
