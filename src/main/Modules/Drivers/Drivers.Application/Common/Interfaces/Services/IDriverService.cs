using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Drivers.Application.Common.Interfaces.Services;

public interface IDriverService
{
    Task<Result> ExistsAndIsActiveAsync(
        Guid id,
        CancellationToken cancellationToken);
}
