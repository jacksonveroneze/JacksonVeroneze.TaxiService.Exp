using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Modules.Rides.Application.v1.Commands.Ride;
using TaxiService.Modules.Rides.Application.v1.Interfaces.Repositories.Ride;
using TaxiService.Modules.Rides.Domain.Core.Errors;
using TaxiService.Modules.Rides.Domain.Entities;

namespace TaxiService.Modules.Rides.Application.v1.Handlers.CommandHandler.Ride;

public sealed class StartCommandHandler(
    ILogger<StartCommandHandler> logger,
    IRideReadRepository rideReadRepository,
    IRideWriteRepository rideWriteRepository)
    : IRequestHandler<StartCommand, Result>
{
    public async Task<Result> Handle(
        StartCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        Result<RideEntity> resultEntity = await GetRideByIdAsync(
            request.Id, cancellationToken);

        if (resultEntity.IsFailure)
        {
            return resultEntity;
        }

        Result resultAction = Start(resultEntity.Value!);

        if (resultAction.IsFailure)
        {
            return resultAction;
        }

        await UpdateAsync(resultEntity.Value!, cancellationToken);

        return Result.WithSuccess();
    }

    private async Task<Result<RideEntity>> GetRideByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        RideEntity? entity = await rideReadRepository
            .GetByIdAsync(id, cancellationToken);

        if (entity is not null)
        {
            return Result<RideEntity>.WithSuccess(entity);
        }

        Error error = DomainErrors.RideError.NotFound;

        logger.LogNotFound(
            nameof(CancelCommandHandler),
            nameof(GetRideByIdAsync), id, error);

        return Result<RideEntity>.FromNotFound(error);
    }

    private Result Start(RideEntity entity)
    {
        Result result = entity.Start();

        if (result.IsFailure)
        {
            logger.LogGenericError(
                nameof(StartCommandHandler),
                nameof(Start), entity.Id, result.Error!);
        }

        return result;
    }

    private async Task UpdateAsync(
        RideEntity entity,
        CancellationToken cancellationToken)
    {
        await rideWriteRepository.UpdateAsync(
            entity, cancellationToken);

        logger.LogProcessed(
            nameof(StartCommandHandler),
            nameof(Handle), entity.Id);
    }
}