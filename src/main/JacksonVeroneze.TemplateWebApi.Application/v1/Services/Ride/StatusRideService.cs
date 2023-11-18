using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.Ride;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Services.Ride;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.Base;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Services.Ride;

public sealed class StatusRideService : IStatusRideService
{
    private readonly ILogger<GetRideService> _logger;
    private readonly IRideWriteRepository _writeRepository;

    public StatusRideService(
        ILogger<GetRideService> logger,
        IRideWriteRepository writeRepository)
    {
        _logger = logger;
        _writeRepository = writeRepository;
    }

    public async Task<IResult> TryAcceptAsync(RideEntity ride, UserEntity user,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(ride);

        IResult result = ride.Accept(user);

        if (result.IsFailure)
        {
            _logger.LogGenericError(nameof(StatusRideService),
                nameof(TryAcceptAsync), ride.Id, result.Error!);

            return Result.Invalid(result.Error!);
        }

        await _writeRepository.UpdateAsync(ride, cancellationToken);

        _logger.LogProcessed(nameof(StatusRideService),
            nameof(TryStartAsync), ride.Id);

        return Result<VoidResponse>.Success();
    }

    public async Task<IResult> TryStartAsync(RideEntity ride,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(ride);

        IResult result = ride.Start();

        if (result.IsFailure)
        {
            _logger.LogGenericError(nameof(StatusRideService),
                nameof(TryStartAsync), ride.Id, result.Error!);

            return Result.Invalid(result.Error!);
        }

        await _writeRepository.UpdateAsync(ride, cancellationToken);

        _logger.LogProcessed(nameof(StatusRideService),
            nameof(TryStartAsync), ride.Id);

        return Result<VoidResponse>.Success();
    }

    public async Task<IResult> TryFinishAsync(RideEntity ride,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(ride);

        IResult result = ride.Finish();

        if (result.IsFailure)
        {
            _logger.LogGenericError(nameof(StatusRideService),
                nameof(TryFinishAsync), ride.Id, result.Error!);

            return Result.Invalid(result.Error!);
        }

        await _writeRepository.UpdateAsync(ride, cancellationToken);

        _logger.LogProcessed(nameof(StatusRideService),
            nameof(TryFinishAsync), ride.Id);

        return Result<VoidResponse>.Success();
    }

    public async Task<IResult> TryCancelAsync(RideEntity ride,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(ride);

        IResult result = ride.Cancel();

        if (result.IsFailure)
        {
            _logger.LogGenericError(nameof(StatusRideService),
                nameof(TryCancelAsync), ride.Id, result.Error!);

            return Result.Invalid(result.Error!);
        }

        await _writeRepository.UpdateAsync(ride, cancellationToken);

        _logger.LogProcessed(nameof(StatusRideService),
            nameof(TryCancelAsync), ride.Id);

        return Result<VoidResponse>.Success();
    }
}
