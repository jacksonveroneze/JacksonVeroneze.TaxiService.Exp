using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TaxiService.Common.SharedKernel.Builders;
using TaxiService.Modules.Driver.Application.v1.Driver.Common.Filters;
using TaxiService.Modules.Driver.Application.v1.Driver.Common.Models;

namespace TaxiService.Modules.Driver.Infrastructure.Builders.Filters;

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
