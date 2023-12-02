using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using JacksonVeroneze.TaxiService.Exp.Domain.Specifications.Base;

namespace JacksonVeroneze.TaxiService.Exp.Domain.Specifications.User;

[ExcludeFromCodeCoverage]
public class UserNameSpecification : BaseSpecification<UserEntity>
{
    private readonly Expression<Func<UserEntity, bool>> _defaultExpression = _ => true;

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
            return _defaultExpression;
        }

        return spec => _matchExactly
            ? spec.Name.Value!.ToUpper().Equals(_name.ToUpper())
            : spec.Name.Value!.ToUpper().Contains(_name.ToUpper());
    }
}
