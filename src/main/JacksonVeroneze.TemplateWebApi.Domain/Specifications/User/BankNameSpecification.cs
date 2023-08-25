using System.Linq.Expressions;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Specifications.Base;

namespace JacksonVeroneze.TemplateWebApi.Domain.Specifications.User;

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
            return spec => true;
        }

        return spec => _matchExactly
            ? spec.Name.Value!.Equals(_name, StringComparison.OrdinalIgnoreCase)
            : spec.Name.Value!.Contains(_name);
    }

    protected override Func<UserEntity, bool> ToFunc()
    {
        return spec => string.IsNullOrEmpty(_name) ||
                       spec.Name.Value!.Contains(_name, StringComparison.OrdinalIgnoreCase);
    }
}
