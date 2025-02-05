using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Drivers.PublicApi.Interfaces;

public interface IDriverService
{
    Task<Result<bool>> ExistsByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);
}