using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Application.Extensions;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Commands.Ride;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.Ride;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.User;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Ride;
using JacksonVeroneze.TaxiService.Exp.Domain.Core.Errors;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Handlers.CommandHandler.Ride;

public sealed class RequestRideCommandHandler(
    ILogger<RequestRideCommandHandler> logger,
    IMapper mapper,
    IUserReadRepository userReadRepository,
    IRideReadRepository rideReadRepository,
    IRideWriteRepository rideWriteRepository)
    : IRequestHandler<RequestRideCommand, Result<RequestRideCommandResponse>>
{
    public async Task<Result<RequestRideCommandResponse>> Handle(
        RequestRideCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        Result<UserEntity> resultUser = await GetUserByIdAsync(
            request.UserId, cancellationToken);

        if (resultUser.IsFailure)
        {
            return Result<RequestRideCommandResponse>.WithError(
                resultUser.Error!);
        }

        Result existsRideByUser = await ExistsActiveRideByUserIdAsync(
            request.UserId, cancellationToken);

        if (existsRideByUser.IsFailure)
        {
            return Result<RequestRideCommandResponse>
                .FromInvalid(existsRideByUser.Error!);
        }

        Result<RideEntity> entity = FactoryEntity(
            request, resultUser.Value!);

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
        RequestRideCommand request, UserEntity user)
    {
        Result<RideEntity> entity = RideEntity.Create(
            user.Id,
            request.LatitudeFrom, request.LatitudeTo,
            request.LongitudeFrom, request.LongitudeTo);

        if (!entity.IsFailure)
        {
            return entity;
        }

        logger.LogGenericError(nameof(RequestRideCommandHandler),
            nameof(Handle), entity.Errors!.Count());

        return Result<RideEntity>
            .FromInvalid(entity.Errors!);
    }

    private async Task<Result<UserEntity>> GetUserByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        UserEntity? driver = await userReadRepository
            .GetByIdAsync(id, cancellationToken);

        if (driver is null)
        {
            return Result<UserEntity>
                .WithError(DomainErrors.UserError.NotFound);
        }

        return Result<UserEntity>.WithSuccess(driver);
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

        logger.LogAlreadyExists(nameof(RequestRideCommandHandler),
            nameof(Handle), id, DomainErrors.RideError.AlreadyByUser);

        return Result.FromInvalid(
            DomainErrors.RideError.AlreadyByUser);
    }

    private async Task CreateAsync(
        RideEntity ride,
        CancellationToken cancellationToken)
    {
        await rideWriteRepository.CreateAsync(
            ride, cancellationToken);

        logger.LogCreated(nameof(RequestRideCommandHandler),
            nameof(Handle), ride.Id);
    }
}