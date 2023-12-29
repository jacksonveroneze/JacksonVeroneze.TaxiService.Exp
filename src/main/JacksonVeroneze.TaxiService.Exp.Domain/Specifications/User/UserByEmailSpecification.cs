using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using JacksonVeroneze.TaxiService.Exp.Domain.Specifications.Base;

namespace JacksonVeroneze.TaxiService.Exp.Domain.Specifications.User;

[ExcludeFromCodeCoverage]
public class UserByEmailSpecification(string email) :
    BaseSpecification<UserEntity>
{
    public override Expression<Func<UserEntity, bool>> ToExpression()
    {
        return spec => spec.Emails
            .Any(item => item.Email.Value!.ToUpper()
                .Equals(email.ToUpper()));
    }
}