using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using TaxiService.Common.Domain.Specifications.Base;
using TaxiService.Modules.Passengers.Domain.Entities;

namespace TaxiService.Modules.Passengers.Domain.Specifications.Email;

[ExcludeFromCodeCoverage]
public class EmailByUserIdSpecification(Guid? userId) :
    BaseSpecification<EmailEntity>
{
    public override Expression<Func<EmailEntity, bool>> ToExpression()
    {
        return userId.HasValue ? spec => spec.UserId == userId! : DefaultExpression;
    }
}