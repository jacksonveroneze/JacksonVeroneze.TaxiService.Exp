using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using TaxiService.Common.BaseDomain.Specifications.Base;
using TaxiService.Modules.Passengers.Domain.Entities;

namespace TaxiService.Modules.Passengers.Domain.Specifications.Email;

[ExcludeFromCodeCoverage]
public class EmailByPassangerIdSpecification(Guid? passangerId) :
    BaseSpecification<EmailEntity>
{
    public override Expression<Func<EmailEntity, bool>> ToExpression()
    {
        return passangerId.HasValue ? spec => spec.PassangerId == passangerId! : DefaultExpression;
    }
}