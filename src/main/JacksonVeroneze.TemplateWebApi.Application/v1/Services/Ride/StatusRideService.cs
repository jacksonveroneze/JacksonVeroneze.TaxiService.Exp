using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.v1.Interfaces.Repositories.Ride;
using JacksonVeroneze.TemplateWebApi.Application.v1.Interfaces.Services.Ride;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.Base;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Services.Ride;

public sealed class StatusRideService(
    ILogger<GetRideService> logger,
    IRideWriteRepository writeRepository)
    : IStatusRideService
{
    public async Task<Result> TryAcceptAsync(
        RideEntity ride, UserEntity user,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(ride);

        Result result = ride.Accept(user);

        if (result.IsFailure)
        {
            logger.LogGenericError(nameof(StatusRideService),
                nameof(TryAcceptAsync), ride.Id, result.Error!);

            return Result.FromInvalid(result.Error!);
        }

        await writeRepository.UpdateAsync(ride, cancellationToken);

        logger.LogProcessed(nameof(StatusRideService),
            nameof(TryStartAsync), ride.Id);

        return Result<VoidResponse>.WithSuccess();
    }

    public async Task<Result> TryStartAsync(
        RideEntity ride,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(ride);

        Result result = ride.Start();

        if (result.IsFailure)
        {
            logger.LogGenericError(nameof(StatusRideService),
                nameof(TryStartAsync), ride.Id, result.Error!);

            return Result.FromInvalid(result.Error!);
        }

        await writeRepository.UpdateAsync(ride, cancellationToken);

        logger.LogProcessed(nameof(StatusRideService),
            nameof(TryStartAsync), ride.Id);

        return Result<VoidResponse>.WithSuccess();
    }

    public async Task<Result> TryFinishAsync(
        RideEntity ride,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(ride);

        Result result = ride.Finish();

        if (result.IsFailure)
        {
            logger.LogGenericError(nameof(StatusRideService),
                nameof(TryFinishAsync), ride.Id, result.Error!);

            return Result.FromInvalid(result.Error!);
        }

        await writeRepository.UpdateAsync(ride, cancellationToken);

        logger.LogProcessed(nameof(StatusRideService),
            nameof(TryFinishAsync), ride.Id);

        return Result<VoidResponse>.WithSuccess();
    }

    public async Task<Result> TryCancelAsync(
        RideEntity ride,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(ride);

        Result result = ride.Cancel();

        if (result.IsFailure)
        {
            logger.LogGenericError(nameof(StatusRideService),
                nameof(TryCancelAsync), ride.Id, result.Error!);

            return Result.FromInvalid(result.Error!);
        }

        await writeRepository.UpdateAsync(ride, cancellationToken);

        logger.LogProcessed(nameof(StatusRideService),
            nameof(TryCancelAsync), ride.Id);

        return Result<VoidResponse>.WithSuccess();
    }
}
