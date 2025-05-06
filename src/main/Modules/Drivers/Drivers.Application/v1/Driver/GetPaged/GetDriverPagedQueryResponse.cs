using TaxiService.Common.BaseApplication.Models.Base.Pagination.Cursor;
using TaxiService.Modules.Drivers.Application.v1.Driver.Common.Models;

namespace TaxiService.Modules.Drivers.Application.v1.Driver.GetPaged;

public sealed record GetDriverPagedQueryResponse
    : PagedResponse<DriverResponse>;
