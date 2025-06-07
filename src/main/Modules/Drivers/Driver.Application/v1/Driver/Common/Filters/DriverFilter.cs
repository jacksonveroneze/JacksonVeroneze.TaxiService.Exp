using System.Diagnostics.CodeAnalysis;
using TaxiService.Modules.Driver.Domain.Enums;

namespace TaxiService.Modules.Driver.Application.v1.Driver.Common.Filters;

[ExcludeFromCodeCoverage]
public record DriverFilter
{
    public ICollection<Guid>? Ids { get; init; }
    
    public DriverStatus? Status { get; init; }
}
