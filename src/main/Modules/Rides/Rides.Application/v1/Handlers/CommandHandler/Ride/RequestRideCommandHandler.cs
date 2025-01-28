using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Modules.Passengers.PublicApi.Interfaces;
using TaxiService.Modules.Rides.Application.v1.Commands.Ride;
using TaxiService.Modules.Rides.Application.v1.Interfaces.Repositories.Ride;
using TaxiService.Modules.Rides.Application.v1.Models.Ride;
using TaxiService.Modules.Rides.Domain.Core.Errors;
using TaxiService.Modules.Rides.Domain.Entities;

namespace TaxiService.Modules.Rides.Application.v1.Handlers.CommandHandler.Ride;

public sealed class RequestRideCommandHandler(
    ILogger<RequestRideCommandHandler> logger,
    IMapper mapper,
    IUserService userService,
    IRideReadRepository rideReadRepository,
    IRideWriteRepository rideWriteRepository)
    : IRequestHandler<RequestRideCommand, Result<RequestRideCommandResponse>>
{
    public async Task<Result<RequestRideCommandResponse>> Handle(
        RequestRideCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        Result<bool> resultUser = await GetUserByIdAsync(
            request.UserId, cancellationToken);

        if (resultUser.IsFailure)
        {
            return Result<RequestRideCommandResponse>
                .WithError(resultUser.Error!);
        }

        Result existsRideByUser = await ExistsActiveRideByUserIdAsync(
            request.UserId, cancellationToken);

        if (existsRideByUser.IsFailure)
        {
            return Result<RequestRideCommandResponse>
                .FromInvalid(existsRideByUser.Error!);
        }

        Result<RideEntity> entity = FactoryEntity(
            request, request.UserId);

        if (entity.IsFailure)
        {
            return Result<RequestRideCommandResponse>
                .FromInvalid(entity.Errors!);
        }

        await CreateAsync(entity.Value!, cancellationToken);

        RequestRideCommandResponse response =
            mapper.Map<RequestRideCommandResponse>(entity.Value);

        return Result<RequestRideCommandResponse>.WithSuccess(response);
    }

    private Result<RideEntity> FactoryEntity(
        RequestRideCommand request, Guid userId)
    {
        Result<RideEntity> entity = RideEntity.Create(
            userId,
            request.LatitudeFrom, request.LatitudeTo,
            request.LongitudeFrom, request.LongitudeTo);

        if (entity.IsSuccess)
        {
            return entity;
        }

        logger.LogGenericError(
            nameof(RequestRideCommandHandler),
            nameof(Handle), entity.Errors!.Count());

        return Result<RideEntity>
            .FromInvalid(entity.Errors!);
    }

    private async Task<Result<bool>> GetUserByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        Result<bool> entity = await userService
            .ExistsByIdAsync(id, cancellationToken);

        return entity;
    }

    private async Task<Result> ExistsActiveRideByUserIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        bool existsRideByUser = await rideReadRepository
            .ExistsActiveByUserIdAsync(id, cancellationToken);

        if (!existsRideByUser)
        {
            return Result.WithSuccess();
        }

        Error error = DomainErrors.RideError.AlreadyByUser;

        logger.LogAlreadyExists(
            nameof(RequestRideCommandHandler),
            nameof(Handle), id, error);

        return Result.FromInvalid(error);
    }

    private async Task CreateAsync(
        RideEntity entity,
        CancellationToken cancellationToken)
    {
        await rideWriteRepository.CreateAsync(
            entity, cancellationToken);

        logger.LogCreated(
            nameof(RequestRideCommandHandler),
            nameof(CreateAsync), entity.Id);
    }
}