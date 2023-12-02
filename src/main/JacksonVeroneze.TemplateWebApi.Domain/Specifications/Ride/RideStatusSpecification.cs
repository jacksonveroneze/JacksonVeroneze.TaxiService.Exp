using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Enums;
using JacksonVeroneze.TemplateWebApi.Domain.Specifications.Base;

namespace JacksonVeroneze.TemplateWebApi.Domain.Specifications.Ride;

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
