using System.Diagnostics;
using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Modules.Passengers.PublicApi.Interfaces;
using TaxiService.Modules.Rides.Application.v1.Commands.Ride;
using TaxiService.Modules.Rides.Application.v1.Interfaces.Repositories.Ride;
using TaxiService.Modules.Rides.Application.v1.Models.Ride;
using TaxiService.Modules.Rides.Domain.Core.Errors;
using TaxiService.Modules.Rides.Domain.Entities;

namespace TaxiService.Modules.Rides.Application.v1.Handlers.CommandHandler.Ride;

public sealed class RequestCommandHandler(
    ILogger<RequestCommandHandler> logger,
    IMapper mapper,
    IUserService userService,
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

        Task<Result> existsRideByUserTask = ExistsActiveRideByUserIdAsync(
            request.UserId, cancellationToken);

        Result<bool> resultUser = await GetUserByIdAsync(
            request.UserId, cancellationToken);

        if (resultUser.IsFailure)
        {
            return Result<RequestCommandResponse>
                .WithError(resultUser.Error!);
        }

        Result existsRideByUser = await existsRideByUserTask;

        if (existsRideByUser.IsFailure)
        {
            return Result<RequestCommandResponse>
                .FromInvalid(existsRideByUser.Error!);
        }

        Result<RideEntity> entity = FactoryEntity(
            request, request.UserId);

        if (entity.IsFailure)
        {
            return Result<RequestCommandResponse>
                .FromInvalid(entity.Errors!);
        }

        await CreateAsync(entity.Value!, cancellationToken);

        RequestCommandResponse response =
            mapper.Map<RequestCommandResponse>(entity.Value);

        return Result<RequestCommandResponse>.WithSuccess(response);
    }

    private Result<RideEntity> FactoryEntity(
        RequestCommand request, Guid userId)
    {
        using Activity? activity = ActivitySource.StartActivity();

        Result<RideEntity> entity = RideEntity.Create(
            userId,
            request.LatitudeFrom, request.LatitudeTo,
            request.LongitudeFrom, request.LongitudeTo);

        if (entity.IsSuccess)
        {
            return entity;
        }

        logger.LogGenericError(
            nameof(RequestCommandHandler),
            nameof(Handle), entity.Errors!.Count());

        return Result<RideEntity>
            .FromInvalid(entity.Errors!);
    }

    private async Task<Result<bool>> GetUserByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        using Activity? activity = ActivitySource.StartActivity();

        Result<bool> entity = await userService
            .ExistsByIdAsync(id, cancellationToken);

        return entity;
    }

    private async Task<Result> ExistsActiveRideByUserIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        using Activity? activity = ActivitySource.StartActivity();

        bool existsRideByUser = await rideReadRepository
            .ExistsActiveByUserIdAsync(id, cancellationToken);

        if (!existsRideByUser)
        {
            return Result.WithSuccess();
        }

        Error error = DomainErrors.RideError.AlreadyByUser;

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