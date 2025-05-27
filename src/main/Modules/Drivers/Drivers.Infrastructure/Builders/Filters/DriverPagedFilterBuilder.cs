using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using TaxiService.Common.BaseInfrastructure;
using TaxiService.Modules.Drivers.Application.v1.Driver.Common.Models;
using TaxiService.Modules.Drivers.Domain.Filters;

namespace TaxiService.Modules.Drivers.Infrastructure.Builders.Filters;

public class DriverPagedFilterBuilder(
    DriverPagedFilter filter)
{
    public static DriverPagedFilterBuilder Create(
        DriverPagedFilter filter)
    {
        return new DriverPagedFilterBuilder(filter);
    }

    public Expression<Func<DriverDto, bool>> Build()
    {
        FilterBuilder<DriverDto> builder = new();

        if (!string.IsNullOrWhiteSpace(filter.Cursor)
            && Guid.TryParse(filter.Cursor, out Guid cursor))
        {
            bool? isNext = filter.Pagination?.PaginationNext;

            if (isNext is true)
            {
                builder.And(d => d.Id > cursor);
            }
            else
            {
                builder.And(d => d.Id < cursor);
            }
        }

        if (!string.IsNullOrWhiteSpace(filter.Name))
        {
            builder.And(d => EF.Functions
                .ILike(d.FullName!, $"%{filter.Name}%"));
        }

        if (filter.Status.HasValue)
        {
            builder.And(d => d.Status == filter.Status);
        }

        return builder.Build();
    }
}
