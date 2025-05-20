using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using TaxiService.Common.BaseDomain.Specifications.Base;
using TaxiService.Modules.Passengers.Domain.Entities;

namespace TaxiService.Modules.Passengers.Domain.Specifications.Email;

[ExcludeFromCodeCoverage]
public class EmailByPassengerIdSpecification(Guid? passengerId) :
    BaseSpecification<EmailEntity>
{
    public override Expression<Func<EmailEntity, bool>> ToExpression()
    {
        return passengerId.HasValue ? spec => spec.PassengerId == passengerId! : DefaultExpression;
    }
}
