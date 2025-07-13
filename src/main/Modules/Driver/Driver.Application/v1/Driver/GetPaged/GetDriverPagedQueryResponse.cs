using TaxiService.Common.BaseApplication.Models.Pagination.Cursor;
using TaxiService.Modules.Driver.Application.v1.Driver.Common.Models;

namespace TaxiService.Modules.Driver.Application.v1.Driver.GetPaged;

public sealed record GetDriverPagedQueryResponse
    : PagedResponse<DriverResponse>;
