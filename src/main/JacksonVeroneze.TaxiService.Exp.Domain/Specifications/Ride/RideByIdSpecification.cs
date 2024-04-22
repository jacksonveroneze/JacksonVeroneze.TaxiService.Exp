using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using JacksonVeroneze.TaxiService.Exp.Domain.Specifications.Base;

namespace JacksonVeroneze.TaxiService.Exp.Domain.Specifications.Ride;

[ExcludeFromCodeCoverage]
public class RideByIdSpecification(Guid id)
    : BaseSpecification<RideEntity>
{
    public override Expression<Func<RideEntity, bool>> ToExpression()
    {
        return spec => spec.Id == id;
    }
}