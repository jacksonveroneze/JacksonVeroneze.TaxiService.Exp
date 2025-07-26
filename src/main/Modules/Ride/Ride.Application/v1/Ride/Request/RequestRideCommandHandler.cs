using JacksonVeroneze.NET.Result;
using Ride.Common;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Common.SharedKernel.Interfaces;
using TaxiService.Modules.Passenger.Domain.Enums;
using TaxiService.Modules.Passenger.PublicApi.v1.Interfaces;
using TaxiService.Modules.Passenger.PublicApi.v1.Models;
using TaxiService.Modules.Ride.Application.Common.Interfaces;
using TaxiService.Modules.Ride.Application.Common.Interfaces.Repositories;
using TaxiService.Modules.Ride.Domain.Core.Errors;
using TaxiService.Modules.Ride.Domain.Repositories;

namespace TaxiService.Modules.Ride.Application.v1.Ride.Request;

public sealed class RequestRideCommandHandler(
    ILogger<RequestRideCommandHandler> logger,
    IDataMapper mapper,
    IPassengerPublicApi passengerPublicApi,
    IRideReadRepository rideReadRepository,
    IRideRepository rideRepository,
    IDomainEventPublisher eventPublisher,
    IMetricsPublisher metricsPublisher)
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

        Task<Result> resultPassengerTask = ValidatePassengerByIdAsync(
            request.PassengerId, cancellationToken);

        await Task.WhenAll(existsRideByPassengerTask, resultPassengerTask);

        Result existsRideByPassenger = await existsRideByPassengerTask;
        Result resultPassenger = await resultPassengerTask;
        
        if (resultPassenger.IsFailure)
        {
            return Result<RequestRideCommandResponse>
                .FromInvalid(resultPassenger.Errors);
        }

        if (existsRideByPassenger.IsFailure)
        {
            return Result<RequestRideCommandResponse>
                .FromRuleViolation(existsRideByPassenger.Errors);
        }

        Result<Domain.Aggregates.Ride> resultEntity = FactoryEntity(
            request, request.PassengerId);

        if (resultEntity.IsFailure)
        {
            return Result<RequestRideCommandResponse>
                .FromInvalid(resultEntity.Errors);
        }

        await CreateAsync(resultEntity.Value!, cancellationToken);

        await eventPublisher.PublishAsync(resultEntity.Value!, cancellationToken);

        metricsPublisher.Counter("ride_requested_total");
        
        RequestRideCommandResponse response =
            mapper.Map<RequestRideCommandResponse>(resultEntity.Value);

        return Result<RequestRideCommandResponse>.WithSuccess(response);
    }

    private Result<Domain.Aggregates.Ride> FactoryEntity(
        RequestRideCommand request, Guid passengerId)
    {
        Result<Domain.Aggregates.Ride> entity = Domain.Aggregates.Ride.Create(
            passengerId,
            request.LatitudeFrom, request.LongitudeFrom,
            request.LatitudeTo, request.LongitudeTo);

        if (entity.IsFailure)
        {
            logger.LogGenericError(
                ModuleConstants.ContextName,
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
            ModuleConstants.ContextName,
            nameof(RequestRideCommandHandler),
            nameof(Handle), id, error);

        return Result.FromInvalid(error);
    }

    private async Task CreateAsync(
        Domain.Aggregates.Ride entity,
        CancellationToken cancellationToken)
    {
        await rideRepository.CreateAsync(
            entity, cancellationToken);

        logger.LogCreated(
            ModuleConstants.ContextName,
            nameof(RequestRideCommandHandler),
            nameof(CreateAsync), entity.Id);
    }
}
