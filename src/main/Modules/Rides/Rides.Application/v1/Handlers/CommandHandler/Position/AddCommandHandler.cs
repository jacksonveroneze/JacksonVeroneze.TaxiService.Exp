using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Modules.Rides.Application.v1.Commands.Position;
using TaxiService.Modules.Rides.Application.v1.Interfaces.Repositories.Position;
using TaxiService.Modules.Rides.Application.v1.Interfaces.Repositories.Ride;
using TaxiService.Modules.Rides.Domain.Core.Errors;
using TaxiService.Modules.Rides.Domain.Entities;

namespace TaxiService.Modules.Rides.Application.v1.Handlers.CommandHandler.Position;

public sealed class AddCommandHandler(
    ILogger<AddCommandHandler> logger,
    IRideReadRepository rideReadRepository,
    IPositionWriteRepository positionWriteRepository)
    : IRequestHandler<AddCommand, Result>
{
    public async Task<Result> Handle(
        AddCommand request,
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
            return Result<AddCommandHandler>
                .FromInvalid(entity.Errors!);
        }

        await CreateAsync(entity.Value!, cancellationToken);

        return Result.WithSuccess();
    }

    private Result<PositionEntity> FactoryEntity(
        AddCommand request)
    {
        Result<PositionEntity> entity = PositionEntity.Create(
            request.RideId, request.Latitude, request.Longitude);

        if (entity.IsSuccess)
        {
            return entity;
        }

        logger.LogGenericError(
            nameof(AddCommandHandler),
            nameof(FactoryEntity), entity.Errors!.Count);

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
            nameof(AddCommandHandler),
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
            nameof(AddCommandHandler),
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
            nameof(AddCommandHandler),
            nameof(CreateAsync), entity.Id);
    }
}
