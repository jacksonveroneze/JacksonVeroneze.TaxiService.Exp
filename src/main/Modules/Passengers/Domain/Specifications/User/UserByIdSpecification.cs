using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using TaxiService.Modules.Passengers.Domain.Entities;
using TaxiService.Common.Domain.Specifications.Base;

namespace TaxiService.Modules.Passengers.Domain.Specifications.User;

[ExcludeFromCodeCoverage]
public class UserByIdSpecification(Guid id) :
    BaseSpecification<UserEntity>
{
    public override Expression<Func<UserEntity, bool>> ToExpression()
    {
        return spec => spec.Id == id;
    }
}