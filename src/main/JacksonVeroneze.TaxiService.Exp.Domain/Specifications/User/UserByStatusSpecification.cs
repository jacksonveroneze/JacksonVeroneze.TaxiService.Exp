using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using JacksonVeroneze.TaxiService.Exp.Domain.Enums;
using JacksonVeroneze.TaxiService.Exp.Domain.Specifications.Base;

namespace JacksonVeroneze.TaxiService.Exp.Domain.Specifications.User;

[ExcludeFromCodeCoverage]
public class UserByStatusSpecification(UserStatus? status) :
    BaseSpecification<UserEntity>
{
    public override Expression<Func<UserEntity, bool>> ToExpression()
    {
        return status.HasValue ? spec => spec.Status == status : DefaultExpression;
    }
}