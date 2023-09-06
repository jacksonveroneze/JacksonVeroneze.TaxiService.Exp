using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Specifications.Base;

namespace JacksonVeroneze.TemplateWebApi.Domain.Specifications.User;

[ExcludeFromCodeCoverage]
public class UserNameSpecification : BaseSpecification<UserEntity>
{
    private readonly bool _matchExactly;

    private readonly string? _name;

    public UserNameSpecification(string? name,
        bool matchExactly = false)
    {
        _name = name;
        _matchExactly = matchExactly;
    }

    public override Expression<Func<UserEntity, bool>> ToExpression()
    {
        if (string.IsNullOrEmpty(_name))
        {
            return _ => true;
        }

        return spec => _matchExactly
            ? spec.Name.Value!.Equals(_name)
            : spec.Name.Value!.Contains(_name);
    }
}
