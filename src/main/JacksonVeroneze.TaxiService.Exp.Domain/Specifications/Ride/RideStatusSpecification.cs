using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using JacksonVeroneze.TaxiService.Exp.Domain.Enums;
using JacksonVeroneze.TaxiService.Exp.Domain.Specifications.Base;

namespace JacksonVeroneze.TaxiService.Exp.Domain.Specifications.Ride;

[ExcludeFromCodeCoverage]
public class RideStatusSpecification : BaseSpecification<RideEntity>
{
    public override Expression<Func<RideEntity, bool>> ToExpression()
    {
        RideStatus[] status =
        {
            RideStatus.Requested,
            RideStatus.Accepted,
            RideStatus.InProgress,
        };

        return spec => status.Contains(spec.Status);
    }
}
