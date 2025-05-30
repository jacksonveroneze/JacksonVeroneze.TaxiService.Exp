using JacksonVeroneze.NET.Result;

using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Modules.Rides.Application.Common.Interfaces.Repositories.Position;
using TaxiService.Modules.Rides.Application.Common.Interfaces.Repositories.Ride;
using TaxiService.Modules.Rides.Domain.Core.Errors;
using TaxiService.Modules.Rides.Domain.Entities;

namespace TaxiService.Modules.Rides.Application.v1.Position.Add;

public sealed class AddPositionCommandHandler(
    ILogger<AddPositionCommandHandler> logger,
    IRideRepository rideRepository,
    IPositionWriteRepository positionWriteRepository)
    : IRequestHandler<AddPositionCommand, Result>
{
    public async Task<Result> Handle(
        AddPositionCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        RideEntity? ride = await rideRepository
            .GetByIdAsync(request.RideId, cancellationToken);

        if (ride is null)
        {
            return Result.FromNotFound(
                DomainErrors.RideError.NotFound);
        }

        Result validateStatus = ValidateStatusRide(ride);

        if (validateStatus.IsFailure)
        {
            return validateStatus;
        }

        Result<PositionEntity> entity = FactoryEntity(request);

        if (entity.IsFailure)
        {
            return Result<AddPositionCommandHandler>
                .FromInvalid(entity.Errors);
        }

        await positionWriteRepository.CreateAsync(
            entity.Value!, cancellationToken);

        logger.LogCreated(
            nameof(AddPositionCommandHandler),
            nameof(Handle), entity.Value!.Id);

        return Result.WithSuccess();
    }

    private Result<PositionEntity> FactoryEntity(
        AddPositionCommand request)
    {
        Result<PositionEntity> entity = PositionEntity.Create(
            request.RideId, request.Latitude, request.Longitude);

        if (entity.IsSuccess)
        {
            return entity;
        }

        logger.LogGenericError(
            nameof(AddPositionCommandHandler),
            nameof(FactoryEntity), entity.Errors.Count);

        return Result<PositionEntity>
            .FromInvalid(entity.Errors);
    }

    private Result ValidateStatusRide(
        RideEntity entity)
    {
        if (entity.InProgress)
        {
            return Result.WithSuccess();
        }

        Error error = DomainErrors.RideError.InvalidStatusAddPosition;

        logger.LogGenericError(
            nameof(AddPositionCommandHandler),
            nameof(ValidateStatusRide), error.Message);

        return Result.WithError(error);
    }
}
