using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Enums;
using JacksonVeroneze.TemplateWebApi.Domain.Specifications.Base;

namespace JacksonVeroneze.TemplateWebApi.Domain.Specifications.Ride;

[ExcludeFromCodeCoverage]
public class RideStatusSpecification : BaseSpecification<RideEntity>
{
    private readonly Expression<Func<RideEntity, bool>> _defaultExpression = _ => true;

    private readonly RideStatus? _status;

    public RideStatusSpecification(RideStatus? status)
    {
        _status = status;
    }

    public override Expression<Func<RideEntity, bool>> ToExpression()
    {
        if (!_status.HasValue)
        {
            return _defaultExpression;
        }

        return spec => spec.Status == _status;
    }
}
