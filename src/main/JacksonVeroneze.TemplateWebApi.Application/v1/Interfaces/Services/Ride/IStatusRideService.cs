using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Interfaces.Services.Ride;

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
        RideEntity ride,
        CancellationToken cancellationToken = default);

    Task<Result> TryCancelAsync(
        RideEntity ride,
        CancellationToken cancellationToken = default);
}
