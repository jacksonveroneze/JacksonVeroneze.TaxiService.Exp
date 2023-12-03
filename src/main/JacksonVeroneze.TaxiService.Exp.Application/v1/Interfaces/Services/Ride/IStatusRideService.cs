using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Services.Ride;

public interface IStatusRideService
{
    Task<Result> TryAcceptAsync(
        RideEntity ride,
        UserEntity user,
        CancellationToken cancellationToken = default);

    Task<Result> TryStartAsync(
        RideEntity ride,
        CancellationToken cancellationToken = default);

    Task<Result> TryFinishAsync(
        RideEntity ride, double distance,
        CancellationToken cancellationToken = default);

    Task<Result> TryCancelAsync(
        RideEntity ride,
        CancellationToken cancellationToken = default);
}
