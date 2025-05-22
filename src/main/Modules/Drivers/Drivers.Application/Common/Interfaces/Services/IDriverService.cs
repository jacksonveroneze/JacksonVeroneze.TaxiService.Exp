using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Drivers.Domain.Entities;

namespace TaxiService.Modules.Drivers.Application.Common.Interfaces.Services;

public interface IDriverService
{
    Task<Result> ExistsAndIsActiveAsync(
        Guid id,
        CancellationToken cancellationToken);
}
