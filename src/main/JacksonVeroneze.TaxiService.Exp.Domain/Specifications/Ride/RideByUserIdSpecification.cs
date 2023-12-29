using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using JacksonVeroneze.TaxiService.Exp.Domain.Specifications.Base;

namespace JacksonVeroneze.TaxiService.Exp.Domain.Specifications.Ride;

[ExcludeFromCodeCoverage]
public class RideByUserIdSpecification(
    Guid? userId) : BaseSpecification<RideEntity>
{
    public override Expression<Func<RideEntity, bool>> ToExpression()
    {
        return userId.HasValue ? spec => spec.UserId == userId : DefaultExpression;
    }
}