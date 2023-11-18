using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;

namespace JacksonVeroneze.TemplateWebApi.Application.Interfaces.Services.Ride;

public interface IStatusRideService
{
    Task<IResult> TryAcceptAsync(
        RideEntity ride,
        UserEntity user,
        CancellationToken cancellationToken = default);

    Task<IResult> TryStartAsync(
        RideEntity ride,
        CancellationToken cancellationToken = default);

    Task<IResult> TryFinishAsync(
        RideEntity ride,
        CancellationToken cancellationToken = default);

    Task<IResult> TryCancelAsync(
        RideEntity ride,
        CancellationToken cancellationToken = default);
}
