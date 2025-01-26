using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Passengers.PublicApi.Models;

namespace TaxiService.Modules.Passengers.PublicApi.Interfaces;

public interface IUserService
{
    Task<Result<UserResponse>> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);
}