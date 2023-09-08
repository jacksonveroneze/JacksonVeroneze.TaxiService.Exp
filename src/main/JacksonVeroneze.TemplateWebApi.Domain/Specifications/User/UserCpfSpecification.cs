using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Specifications.Base;

namespace JacksonVeroneze.TemplateWebApi.Domain.Specifications.User;

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
