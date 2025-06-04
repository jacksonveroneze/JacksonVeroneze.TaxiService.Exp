using JacksonVeroneze.NET.Result;

using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Modules.Rides.Domain.Aggregates;
using TaxiService.Modules.Rides.Domain.Core.Errors;
using TaxiService.Modules.Rides.Domain.Entities;
using TaxiService.Modules.Rides.Domain.Repositories;

namespace TaxiService.Modules.Rides.Application.v1.Position.Add;

public sealed class AddPositionCommandHandler(
    ILogger<AddPositionCommandHandler> logger,
    IRideRepository rideRepository,
    IPositionRepository positionRepository)
    : IRequestHandler<AddPositionCommand, Result>
{
    public async Task<Result> Handle(
        AddPositionCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        Domain.Aggregates.Ride? ride = await rideRepository
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

        Result<Domain.Entities.Position> entity = FactoryEntity(request);

        if (entity.IsFailure)
        {
            return Result<AddPositionCommandHandler>
                .FromInvalid(entity.Errors);
        }

        await positionRepository.CreateAsync(
            entity.Value!, cancellationToken);

        logger.LogCreated(
            nameof(AddPositionCommandHandler),
            nameof(Handle), entity.Value!.Id);

        return Result.WithSuccess();
    }

    private Result<Domain.Entities.Position> FactoryEntity(
        AddPositionCommand request)
    {
        Result<Domain.Entities.Position> entity = Domain.Entities.Position.Create(
            request.RideId, request.Latitude, request.Longitude);

        if (entity.IsSuccess)
        {
            return entity;
        }

        logger.LogGenericError(
            nameof(AddPositionCommandHandler),
            nameof(FactoryEntity), entity.Errors.Count);

        return Result<Domain.Entities.Position>
            .FromInvalid(entity.Errors);
    }

    private Result ValidateStatusRide(
        Domain.Aggregates.Ride entity)
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
