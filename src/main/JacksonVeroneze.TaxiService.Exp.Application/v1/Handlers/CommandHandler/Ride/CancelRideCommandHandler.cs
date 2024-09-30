using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Application.Extensions;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Commands.Ride;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.Ride;
using JacksonVeroneze.TaxiService.Exp.Domain.Core.Errors;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Handlers.CommandHandler.Ride;

public sealed class CancelRideCommandHandler(
    ILogger<CancelRideCommandHandler> logger,
    IRideReadRepository rideReadRepository,
    IRideWriteRepository rideWriteRepository)
    : IRequestHandler<CancelRideCommand, Result>
{
    public async Task<Result> Handle(
        CancelRideCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        Result<RideEntity> resultRide = await GetRideByIdAsync(
            request.Id, cancellationToken);

        if (resultRide.IsFailure)
        {
            return resultRide;
        }

        Result resultAction = Cancel(resultRide.Value!);

        if (resultAction.IsFailure)
        {
            return resultAction;
        }

        await UpdateAsync(resultRide.Value!, cancellationToken);

        return Result.WithSuccess();
    }

    private async Task<Result<RideEntity>> GetRideByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        RideEntity? ride = await rideReadRepository
            .GetByIdAsync(id, cancellationToken);

        if (ride is null)
        {
            return Result<RideEntity>.FromNotFound(
                DomainErrors.RideError.NotFound);
        }

        return Result<RideEntity>.WithSuccess(ride);
    }

    private Result Cancel(RideEntity ride)
    {
        Result result = ride.Cancel();

        if (result.IsSuccess)
        {
            return Result.WithSuccess();
        }

        logger.LogGenericError(nameof(CancelRideCommandHandler),
            nameof(Handle), ride.Id, result.Error!);

        return result;
    }

    private async Task UpdateAsync(
        RideEntity ride,
        CancellationToken cancellationToken)
    {
        await rideWriteRepository.UpdateAsync(
            ride, cancellationToken);

        logger.LogProcessed(nameof(CancelRideCommandHandler),
            nameof(Handle), ride.Id);
    }
}