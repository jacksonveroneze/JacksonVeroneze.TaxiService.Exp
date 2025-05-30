using JacksonVeroneze.NET.Result;

using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Common.BaseApplication.Interfaces.Mapper;
using TaxiService.Modules.Passengers.Domain.Enums;
using TaxiService.Modules.Passengers.PublicApi.v1.Interfaces;
using TaxiService.Modules.Passengers.PublicApi.v1.Models;
using TaxiService.Modules.Rides.Application.Common.Interfaces.Repositories.Ride;
using TaxiService.Modules.Rides.Domain.Core.Errors;
using TaxiService.Modules.Rides.Domain.Entities;

namespace TaxiService.Modules.Rides.Application.v1.Ride.Request;

public sealed class RequestRideCommandHandler(
    ILogger<RequestRideCommandHandler> logger,
    IDataMapper mapper,
    IPassengerPublicApi passengerPublicApi,
    IRideReadRepository rideReadRepository,
    IRideRepository rideRepository)
    : IRequestHandler<RequestRideCommand, Result<RequestRideCommandResponse>>
{
    public async Task<Result<RequestRideCommandResponse>> Handle(
        RequestRideCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        Task<Result> existsRideByPassengerTask =
            ExistsActiveRideByPassengerIdAsync(
                request.PassengerId, cancellationToken);

        Result resultPassenger = await ValidatePassengerByIdAsync(
            request.PassengerId, cancellationToken);

        if (resultPassenger.IsFailure)
        {
            return Result<RequestRideCommandResponse>
                .FromInvalid(resultPassenger.Errors);
        }

        Result existsRideByPassenger = await existsRideByPassengerTask;

        if (existsRideByPassenger.IsFailure)
        {
            return Result<RequestRideCommandResponse>
                .FromRuleViolation(existsRideByPassenger.Errors);
        }

        Result<RideEntity> resultEntity = FactoryEntity(
            request, request.PassengerId);

        if (resultEntity.IsFailure)
        {
            return Result<RequestRideCommandResponse>
                .FromInvalid(resultEntity.Errors);
        }

        await CreateAsync(resultEntity.Value!, cancellationToken);

        RequestRideCommandResponse response =
            mapper.Map<RequestRideCommandResponse>(resultEntity.Value);

        return Result<RequestRideCommandResponse>.WithSuccess(response);
    }

    private Result<RideEntity> FactoryEntity(
        RequestRideCommand request, Guid passengerId)
    {
        Result<RideEntity> entity = RideEntity.Create(
            passengerId,
            request.LatitudeFrom, request.LatitudeTo,
            request.LongitudeFrom, request.LongitudeTo);

        if (entity.IsFailure)
        {
            logger.LogGenericError(
                nameof(RequestRideCommandHandler),
                nameof(Handle), entity.Errors.Count);
        }

        return entity;
    }

    private async Task<Result> ValidatePassengerByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        PassengerPublicApiDto? passenger = await passengerPublicApi
            .GetByIdAsync(id, cancellationToken);

        if (passenger is null)
        {
            return Result.FromConflict(
                DomainErrors.RideError.PassengerNotFound);
        }

        if (passenger.Status != PassengerStatus.Active)
        {
            return Result.FromRuleViolation(
                DomainErrors.RideError.PassengerInactive);
        }

        return Result.WithSuccess();
    }

    private async Task<Result> ExistsActiveRideByPassengerIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        bool existsRideByPassenger = await rideReadRepository
            .ExistsActiveByPassengerIdAsync(id, cancellationToken);

        if (!existsRideByPassenger)
        {
            return Result.WithSuccess();
        }

        Error error = DomainErrors.RideError.AlreadyByPassenger;

        logger.LogAlreadyExists(
            nameof(RequestRideCommandHandler),
            nameof(Handle), id, error);

        return Result.FromInvalid(error);
    }

    private async Task CreateAsync(
        RideEntity entity,
        CancellationToken cancellationToken)
    {
        await rideRepository.CreateAsync(
            entity, cancellationToken);

        logger.LogCreated(
            nameof(RequestRideCommandHandler),
            nameof(CreateAsync), entity.Id);
    }
}
