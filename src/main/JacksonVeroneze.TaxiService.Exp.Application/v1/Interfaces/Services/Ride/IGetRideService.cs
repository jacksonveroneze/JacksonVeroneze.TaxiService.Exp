using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Services.Ride;

public interface IGetRideService
{
    Task<Result<RideEntity>> TryGetRideAsync(
        Guid id,
        CancellationToken cancellationToken = default);
}
