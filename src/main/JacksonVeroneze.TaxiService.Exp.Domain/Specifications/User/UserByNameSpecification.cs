using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using JacksonVeroneze.TaxiService.Exp.Domain.Specifications.Base;

namespace JacksonVeroneze.TaxiService.Exp.Domain.Specifications.User;

[ExcludeFromCodeCoverage]
public class UserByNameSpecification(
    string? name,
    bool matchExactly = false) : BaseSpecification<UserEntity>
{
    public override Expression<Func<UserEntity, bool>> ToExpression()
    {
        if (string.IsNullOrEmpty(name))
        {
            return DefaultExpression;
        }

        return spec => matchExactly
            ? spec.Name.Value!.ToUpper()
                .Equals(name.ToUpperInvariant())
            : spec.Name.Value!.ToUpper()
                .Contains(name.ToUpperInvariant());
    }
}