using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Application.Extensions;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Commands.Position;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.Position;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Services.Ride;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Base;
using JacksonVeroneze.TaxiService.Exp.Domain.Core.Errors;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using JacksonVeroneze.TaxiService.Exp.Domain.Enums;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Handlers.CommandHandler.Position;

public sealed class UpdatePositionCommandHandler(
    ILogger<UpdatePositionCommandHandler> logger,
    IGetRideService rideService,
    IPositionWriteRepository repository)
    : IRequestHandler<UpdatePositionCommand, Result<VoidResponse>>
{
    public async Task<Result<VoidResponse>> Handle(
        UpdatePositionCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        Result<RideEntity> ride = await rideService
            .TryGetRideAsync(request.RideId, cancellationToken);

        if (ride.IsFailure)
        {
            return Result<VoidResponse>
                .WithError(ride.Error!);
        }

        if (ride.Value!.Status != RideStatus.InProgress)
        {
            Error error = DomainErrors.Ride.InvalidStatusAddPosition;

            logger.LogGenericError(nameof(UpdatePositionCommandHandler),
                nameof(Handle), error.Message);

            return Result<VoidResponse>
                .WithError(error);
        }

        Result<PositionEntity> entity = PositionEntity.Create(
            ride.Value!, request.Latitude, request.Longitude);

        if (entity.IsFailure)
        {
            logger.LogGenericError(nameof(UpdatePositionCommandHandler),
                nameof(Handle), entity.Errors!.Count());

            return Result<VoidResponse>
                .FromInvalid(entity.Error!);
        }

        await repository.CreateAsync(entity.Value!, cancellationToken);

        logger.LogCreated(nameof(UpdatePositionCommandHandler),
            nameof(Handle), entity.Value!.Id);

        return Result<VoidResponse>.WithSuccess();
    }
}
