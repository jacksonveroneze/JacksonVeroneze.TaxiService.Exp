using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using JacksonVeroneze.TaxiService.Exp.Domain.Specifications.Base;

namespace JacksonVeroneze.TaxiService.Exp.Domain.Specifications.User;

[ExcludeFromCodeCoverage]
public class UserCpfSpecification : BaseSpecification<UserEntity>
{
    private readonly string _cpf;

    public UserCpfSpecification(string cpf)
    {
        _cpf = cpf;
    }

    public override Expression<Func<UserEntity, bool>> ToExpression()
    {
        return spec => spec.Cpf.Value == _cpf;
    }
}
