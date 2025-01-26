using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Passengers.Application.Extensions;
using TaxiService.Modules.Passengers.Application.v1.Commands.Position;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.Position;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.Ride;
using TaxiService.Modules.Passengers.Domain.Entities;
using TaxiService.Modules.Rides.Application.Core.Errors;

namespace TaxiService.Modules.Passengers.Application.v1.Handlers.CommandHandler.Position;

public sealed class AddPositionCommandHandler(
    ILogger<AddPositionCommandHandler> logger,
    IRideReadRepository rideReadRepository,
    IPositionWriteRepository positionWriteRepository)
    : IRequestHandler<AddPositionCommand, Result>
{
    public async Task<Result> Handle(
        AddPositionCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        Result<RideEntity> resultEntity = await GetRideByIdAsync(
            request.RideId, cancellationToken);

        if (resultEntity.IsFailure)
        {
            return resultEntity;
        }

        Result validateStatus = ValidateStatusEntity(resultEntity.Value!);

        if (validateStatus.IsFailure)
        {
            return validateStatus;
        }

        Result<PositionEntity> entity = FactoryEntity(request);

        if (entity.IsFailure)
        {
            return Result<AddPositionCommandHandler>
                .FromInvalid(entity.Errors!);
        }

        await CreateAsync(entity.Value!, cancellationToken);

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
            nameof(FactoryEntity), entity.Errors!.Count());

        return Result<PositionEntity>
            .FromInvalid(entity.Errors!);
    }

    private Result ValidateStatusEntity(
        RideEntity entity)
    {
        if (entity.InProgress)
        {
            return Result.WithSuccess();
        }

        Error error = DomainErrors.RideError.InvalidStatusAddPosition;

        logger.LogGenericError(
            nameof(AddPositionCommandHandler),
            nameof(ValidateStatusEntity), error.Message);

        return Result.WithError(error);
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
            nameof(AddPositionCommandHandler),
            nameof(GetRideByIdAsync), id, error);

        return Result<RideEntity>.FromNotFound(error);
    }

    private async Task CreateAsync(
        PositionEntity entity,
        CancellationToken cancellationToken)
    {
        await positionWriteRepository.CreateAsync(
            entity, cancellationToken);

        logger.LogCreated(
            nameof(AddPositionCommandHandler),
            nameof(CreateAsync), entity.Id);
    }
}