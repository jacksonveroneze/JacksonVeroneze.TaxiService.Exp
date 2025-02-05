using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using TaxiService.Common.BaseDomain.Specifications.Base;
using TaxiService.Modules.Drivers.Domain.Entities;

namespace TaxiService.Modules.Drivers.Domain.Specifications.Driver;

[ExcludeFromCodeCoverage]
public class DriverByIdSpecification(Guid id) :
    BaseSpecification<DriverEntity>
{
    public override Expression<Func<DriverEntity, bool>> ToExpression()
    {
        return spec => spec.Id == id;
    }
}