using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using TaxiService.Common.BaseInfrastructure;
using TaxiService.Modules.Drivers.Application.v1.Driver.Common.Models;
using TaxiService.Modules.Drivers.Domain.Filters;

namespace TaxiService.Modules.Drivers.Infrastructure.Builders.Filters;

public class DriverFilterBuilder(DriverPagedFilter filter)
{
    private readonly DriverPagedFilter _filter =
        filter ?? throw new ArgumentNullException(nameof(filter));

    public static DriverFilterBuilder Create(DriverPagedFilter filter)
    {
        return new DriverFilterBuilder(filter);
    }

    public Expression<Func<DriverDto, bool>> Build()
    {
        FilterBuilder<DriverDto> builder = new();

        if (!string.IsNullOrWhiteSpace(_filter.Cursor)
            && Guid.TryParse(_filter.Cursor, out Guid cursor))
        {
            bool? isNext = _filter.Pagination?.PaginationNext;
 
            if (isNext is true)
            {
                builder.And(d => d.Id > cursor);
            }
            else
            {
                builder.And(d => d.Id < cursor);
            }
        }

        if (!string.IsNullOrWhiteSpace(_filter.Name))
        {
            builder.And(d => EF.Functions
                .ILike(d.FullName!, $"%{_filter.Name}%"));
        }

        if (_filter.Status.HasValue)
        {
            builder.And(d => d.Status == _filter.Status);
        }

        return builder.Build();
    }
}
