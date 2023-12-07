using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using JacksonVeroneze.TaxiService.Exp.Domain.Specifications.Base;

namespace JacksonVeroneze.TaxiService.Exp.Domain.Specifications.User;

[ExcludeFromCodeCoverage]
public class UserNameSpecification(
    string? name,
    bool matchExactly = false) : BaseSpecification<UserEntity>
{
    private readonly Expression<Func<UserEntity, bool>> _defaultExpression = _ => true;

    public override Expression<Func<UserEntity, bool>> ToExpression()
    {
        if (string.IsNullOrEmpty(name))
        {
            return _defaultExpression;
        }

        return spec => matchExactly
            ? spec.Name.Value!.ToUpper().Equals(name.ToUpper(),
                StringComparison.OrdinalIgnoreCase)
            : spec.Name.Value!.ToUpper().Contains(name.ToUpper());
    }
}
