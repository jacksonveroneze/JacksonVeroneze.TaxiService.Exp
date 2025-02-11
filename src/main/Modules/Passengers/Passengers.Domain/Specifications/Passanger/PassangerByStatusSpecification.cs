using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using TaxiService.Common.BaseDomain.Specifications.Base;
using TaxiService.Modules.Passengers.Domain.Entities;
using TaxiService.Modules.Passengers.Domain.Enums;

namespace TaxiService.Modules.Passengers.Domain.Specifications.Passanger;

[ExcludeFromCodeCoverage]
public class PassangerByStatusSpecification(PassangerStatus? status) :
    BaseSpecification<PassangerEntity>
{
    public override Expression<Func<PassangerEntity, bool>> ToExpression()
    {
        return status.HasValue ? spec => spec.Status == status : DefaultExpression;
    }
}