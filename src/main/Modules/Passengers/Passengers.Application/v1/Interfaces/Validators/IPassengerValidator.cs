using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Passengers.Domain.Entities;

namespace TaxiService.Modules.Passengers.Application.v1.Interfaces.Validators;

public interface IPassengerValidator
{
    Task<Result> ExistsAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<Result<PassengerEntity>> ValidateAndGetAsync(
        Guid id,
        CancellationToken cancellationToken);
}
