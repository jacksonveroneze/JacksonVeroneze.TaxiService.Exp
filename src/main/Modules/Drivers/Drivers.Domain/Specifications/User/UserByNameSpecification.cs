using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using TaxiService.Common.BaseDomain.Specifications.Base;
using TaxiService.Modules.Passengers.Domain.Entities;

namespace TaxiService.Modules.Passengers.Domain.Specifications.User;

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
            ? spec.Name!.Value!.ToUpper()
                .Equals(name.ToUpperInvariant())
            : spec.Name!.Value!.ToUpper()
                .Contains(name.ToUpperInvariant());
    }
}