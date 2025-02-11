using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Passengers.PublicApi.Interfaces;

public interface IPassangerService
{
    Task<Result<bool>> ExistsByIdAsync(
        Guid id,
        CancellationToken cancellationToken);
}