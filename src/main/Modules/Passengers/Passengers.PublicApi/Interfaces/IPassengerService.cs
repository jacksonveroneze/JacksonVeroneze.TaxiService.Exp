using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Passengers.Application.v1.Models.Passenger;

namespace TaxiService.Modules.Passengers.PublicApi.Interfaces;

public interface IPassengerService
{
    Task<Result<PassengerResponse>> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);
}
