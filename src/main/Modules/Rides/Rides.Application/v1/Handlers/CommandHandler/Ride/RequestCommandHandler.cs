using System.Diagnostics;
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

public sealed class RequestCommandHandler(
    ILogger<RequestCommandHandler> logger,
    IDataMapper mapper,
    IPassengerService passengerService,
    IRideReadRepository rideReadRepository,
    IRideWriteRepository rideWriteRepository)
    : IRequestHandler<RequestCommand, Result<RequestCommandResponse>>
{
    private static readonly ActivitySource ActivitySource =
        new("TaxiService");

    public async Task<Result<RequestCommandResponse>> Handle(
        RequestCommand request,
        CancellationToken cancellationToken)
    {
        using Activity? activity = ActivitySource.StartActivity();

        ArgumentNullException.ThrowIfNull(request);

        Task<Result> existsRideByPassengerTask =
            ExistsActiveRideByPassengerIdAsync(
                request.PassengerId, cancellationToken);

        Result<bool> resultPassenger = await ValidatePassengerByIdAsync(
            request.PassengerId, cancellationToken);

        if (resultPassenger.IsFailure)
        {
            return Result<RequestCommandResponse>
                .WithError(resultPassenger.FirstError!);
        }

        Result existsRideByPassenger = await existsRideByPassengerTask;

        if (existsRideByPassenger.IsFailure)
        {
            return Result<RequestCommandResponse>
                .FromInvalid(existsRideByPassenger.FirstError!);
        }

        Result<RideEntity> entity = FactoryEntity(
            request, request.PassengerId);

        if (entity.IsFailure)
        {
            return Result<RequestCommandResponse>
                .FromInvalid(entity.Errors);
        }

        await CreateAsync(entity.Value!, cancellationToken);

        RequestCommandResponse response =
            mapper.Map<RequestCommandResponse>(entity.Value);

        return Result<RequestCommandResponse>.WithSuccess(response);
    }

    private Result<RideEntity> FactoryEntity(
        RequestCommand request, Guid passengerId)
    {
        using Activity? activity = ActivitySource.StartActivity();

        Result<RideEntity> entity = RideEntity.Create(
            passengerId,
            request.LatitudeFrom, request.LatitudeTo,
            request.LongitudeFrom, request.LongitudeTo);

        if (entity.IsSuccess)
        {
            return entity;
        }

        logger.LogGenericError(
            nameof(RequestCommandHandler),
            nameof(Handle), entity.Errors.Count);

        return Result<RideEntity>
            .FromInvalid(entity.Errors);
    }

    private async Task<Result<bool>> ValidatePassengerByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        using Activity? activity = ActivitySource.StartActivity();

        Result<PassengerResponse> entity = await passengerService
            .GetByIdAsync(id, cancellationToken);

        if (!entity.IsSuccess)
        {
            return Result<bool>
                .WithError(entity.FirstError!);
        }

        if (entity.Value!.Status != PassengerStatus.Active)
        {
            return Result<bool>
                .WithError(entity.FirstError!);
        }

        return Result<bool>.WithSuccess();
    }

    private async Task<Result> ExistsActiveRideByPassengerIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        using Activity? activity = ActivitySource.StartActivity();

        bool existsRideByPassenger = await rideReadRepository
            .ExistsActiveByPassengerIdAsync(id, cancellationToken);

        if (!existsRideByPassenger)
        {
            return Result.WithSuccess();
        }

        Error error = DomainErrors.RideError.AlreadyByPassenger;

        logger.LogAlreadyExists(
            nameof(RequestCommandHandler),
            nameof(Handle), id, error);

        return Result.FromInvalid(error);
    }

    private async Task CreateAsync(
        RideEntity entity,
        CancellationToken cancellationToken)
    {
        using Activity? activity = ActivitySource.StartActivity();

        await rideWriteRepository.CreateAsync(
            entity, cancellationToken);

        logger.LogCreated(
            nameof(RequestCommandHandler),
            nameof(CreateAsync), entity.Id);
    }
}
