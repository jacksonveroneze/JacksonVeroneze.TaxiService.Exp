using System.Diagnostics.CodeAnalysis;
using TaxiService.Modules.Passengers.Domain.Enums;
using TaxiService.Modules.Passengers.Domain.Filters.Base;
using TaxiService.Modules.Passengers.Domain.Util;

namespace TaxiService.Modules.Passengers.Domain.Filters;

[ExcludeFromCodeCoverage]
public record UserPagedQuery : BaseQuery<UserPagedFilter>;

[ExcludeFromCodeCoverage]
public record UserPagedFilter
{
    public StringFilter? NameFilter { get; set; }

    public DateOnlyFilter? StartBirthdayFilter { get; set; }

    public DateOnlyFilter? EndBirthdayFilter { get; set; }

    public EnumFilter<UserStatus>? StatusFilter { get; set; }
}