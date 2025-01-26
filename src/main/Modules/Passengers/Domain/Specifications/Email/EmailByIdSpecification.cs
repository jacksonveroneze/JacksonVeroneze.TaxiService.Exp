using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using TaxiService.Modules.Passengers.Domain.Entities;
using TaxiService.Modules.Passengers.Domain.Specifications.Base;

namespace TaxiService.Modules.Passengers.Domain.Specifications.Email;

[ExcludeFromCodeCoverage]
public class EmailByIdSpecification(Guid id) :
    BaseSpecification<EmailEntity>
{
    public override Expression<Func<EmailEntity, bool>> ToExpression()
    {
        return spec => spec.Id == id;
    }
}