using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

using TaxiService.Common.BaseDomain.Specifications.Base;
using TaxiService.Modules.Passengers.Domain.Entities;

namespace TaxiService.Modules.Passengers.Domain.Specifications.Passenger;

[ExcludeFromCodeCoverage]
public class PassengerCursorSpecification(Guid id) :
    BaseSpecification<PassengerEntity>
{
    public override Expression<Func<PassengerEntity, bool>> ToExpression()
    {
        return spec => spec.Id > id;
    }
}
