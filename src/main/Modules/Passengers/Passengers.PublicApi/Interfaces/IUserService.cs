using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Passengers.PublicApi.Interfaces;

public interface IUserService
{
    Task<Result<bool>> ExistsByIdAsync(
        Guid id,
        CancellationToken cancellationToken);
}