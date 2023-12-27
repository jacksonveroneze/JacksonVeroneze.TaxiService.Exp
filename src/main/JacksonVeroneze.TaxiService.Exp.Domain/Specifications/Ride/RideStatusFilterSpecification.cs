using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using JacksonVeroneze.TaxiService.Exp.Domain.Enums;
using JacksonVeroneze.TaxiService.Exp.Domain.Specifications.Base;

namespace JacksonVeroneze.TaxiService.Exp.Domain.Specifications.Ride;

[ExcludeFromCodeCoverage]
public class RideStatusFilterSpecification : BaseSpecification<RideEntity>
{
    private readonly Expression<Func<RideEntity, bool>> _defaultExpression = _ => true;

    private readonly RideStatus? _status;

    public RideStatusFilterSpecification(RideStatus? status)
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