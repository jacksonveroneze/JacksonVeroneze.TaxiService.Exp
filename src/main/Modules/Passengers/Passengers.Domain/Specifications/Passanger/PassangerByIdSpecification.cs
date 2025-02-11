using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using TaxiService.Common.BaseDomain.Specifications.Base;
using TaxiService.Modules.Passengers.Domain.Entities;

namespace TaxiService.Modules.Passengers.Domain.Specifications.Passanger;

[ExcludeFromCodeCoverage]
public class PassangerByIdSpecification(Guid id) :
    BaseSpecification<PassangerEntity>
{
    public override Expression<Func<PassangerEntity, bool>> ToExpression()
    {
        return spec => spec.Id == id;
    }
}