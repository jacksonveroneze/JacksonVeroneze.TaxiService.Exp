using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using JacksonVeroneze.TaxiService.Exp.Domain.Specifications.Base;

namespace JacksonVeroneze.TaxiService.Exp.Domain.Specifications.User;

[ExcludeFromCodeCoverage]
public class UserByIdSpecification(Guid id) :
    BaseSpecification<UserEntity>
{
    public override Expression<Func<UserEntity, bool>> ToExpression()
    {
        return spec => spec.Id == id;
    }
}