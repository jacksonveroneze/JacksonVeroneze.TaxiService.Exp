using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Application.Extensions;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Commands.Ride;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.Ride;
using JacksonVeroneze.TaxiService.Exp.Domain.Core.Errors;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Handlers.CommandHandler.Ride;

public sealed class StartRideCommandHandler(
    ILogger<StartRideCommandHandler> logger,
    IRideReadRepository rideReadRepository,
    IRideWriteRepository rideWriteRepository)
    : IRequestHandler<StartRideCommand, Result>
{
    public async Task<Result> Handle(
        StartRideCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        Result<RideEntity> resultRide = await GetRideByIdAsync(
            request.Id, cancellationToken);

        if (resultRide.IsFailure)
        {
            return resultRide;
        }

        Result resultAction = Start(resultRide.Value!);

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

    private Result Start(RideEntity ride)
    {
        Result result = ride.Start();

        if (result.IsSuccess)
        {
            return Result.WithSuccess();
        }

        logger.LogGenericError(nameof(StartRideCommandHandler),
            nameof(Handle), ride.Id, result.Error!);

        return result;
    }

    private async Task UpdateAsync(
        RideEntity ride,
        CancellationToken cancellationToken)
    {
        await rideWriteRepository.UpdateAsync(
            ride, cancellationToken);

        logger.LogProcessed(nameof(StartRideCommandHandler),
            nameof(Handle), ride.Id);
    }
}