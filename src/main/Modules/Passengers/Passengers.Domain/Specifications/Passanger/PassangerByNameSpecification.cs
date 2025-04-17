using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using TaxiService.Common.BaseDomain.Specifications.Base;
using TaxiService.Modules.Passengers.Domain.Entities;

namespace TaxiService.Modules.Passengers.Domain.Specifications.Passanger;

[ExcludeFromCodeCoverage]
public class PassangerByNameSpecification(
    string? name,
    bool matchExactly = false) : BaseSpecification<PassangerEntity>
{
    public override Expression<Func<PassangerEntity, bool>> ToExpression()
    {
        if (string.IsNullOrEmpty(name))
        {
            return DefaultExpression;
        }

        return spec => matchExactly
            ? spec.Name.Value!.ToUpperInvariant()
                .Equals(name.ToUpperInvariant(), StringComparison.OrdinalIgnoreCase)
            : spec.Name.Value!.Contains(name, StringComparison.InvariantCultureIgnoreCase);
    }
}