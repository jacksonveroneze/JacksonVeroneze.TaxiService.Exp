using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Common.BaseApplication.Interfaces.Mapper;
using TaxiService.Modules.Passengers.Application.v1.Models.Passenger;
using TaxiService.Modules.Passengers.Domain.Enums;
using TaxiService.Modules.Passengers.PublicApi.Interfaces;
using TaxiService.Modules.Rides.Application.v1.Commands.Ride;
using TaxiService.Modules.Rides.Application.v1.Interfaces.Repositories.Ride;
using TaxiService.Modules.Rides.Application.v1.Models.Ride;
using TaxiService.Modules.Rides.Domain.Core.Errors;
using TaxiService.Modules.Rides.Domain.Entities;

namespace TaxiService.Modules.Rides.Application.v1.Handlers.CommandHandler.Ride;

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

        Result<bool> resultPassenger = await ValidatePassengerByIdAsync(
            request.PassengerId, cancellationToken);

        if (resultPassenger.IsFailure)
        {
            return Result<RequestRideCommandResponse>
                .WithError(resultPassenger.FirstError!);
        }

        Result existsRideByPassenger = await existsRideByPassengerTask;

        if (existsRideByPassenger.IsFailure)
        {
            return Result<RequestRideCommandResponse>
                .FromInvalid(existsRideByPassenger.FirstError!);
        }

        Result<RideEntity> entity = FactoryEntity(
            request, request.PassengerId);

        if (entity.IsFailure)
        {
            return Result<RequestRideCommandResponse>
                .FromInvalid(entity.Errors);
        }

        await CreateAsync(entity.Value!, cancellationToken);

        RequestRideCommandResponse response =
            mapper.Map<RequestRideCommandResponse>(entity.Value);

        return Result<RequestRideCommandResponse>.WithSuccess(response);
    }

    private Result<RideEntity> FactoryEntity(
        RequestRideCommand request, Guid passengerId)
    {
        Result<RideEntity> entity = RideEntity.Create(
            passengerId,
            request.LatitudeFrom, request.LatitudeTo,
            request.LongitudeFrom, request.LongitudeTo);

        if (entity.IsSuccess)
        {
            return entity;
        }

        logger.LogGenericError(
            nameof(RequestRideCommandHandler),
            nameof(Handle), entity.Errors.Count);

        return Result<RideEntity>
            .FromInvalid(entity.Errors);
    }

    private async Task<Result<bool>> ValidatePassengerByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        PassengerDto? passenger = await passengerPublicApi
            .GetByIdAsync(id, cancellationToken);

        if (passenger is null)
        {
            return Result<bool>
                .FromRuleViolation(DomainErrors.RideError.NotFound);
        }

        if (passenger.Status != PassengerStatus.Active)
        {
            return Result<bool>
                .WithError(DomainErrors.RideError.NotFound);
        }

        return Result<bool>.WithSuccess();
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
