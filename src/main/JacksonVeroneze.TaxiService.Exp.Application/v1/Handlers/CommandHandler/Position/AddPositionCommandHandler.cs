using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Application.Extensions;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Commands.Position;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.Position;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.Ride;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Base;
using JacksonVeroneze.TaxiService.Exp.Domain.Core.Errors;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Handlers.CommandHandler.Position;

public sealed class AddPositionCommandHandler(
    ILogger<AddPositionCommandHandler> logger,
    IRideReadRepository rideReadRepository,
    IPositionWriteRepository positionWriteRepository)
    : IRequestHandler<AddPositionCommand, Result<VoidResponse>>
{
    public async Task<Result<VoidResponse>> Handle(
        AddPositionCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        RideEntity? ride = await rideReadRepository
            .GetByIdAsync(request.RideId, cancellationToken);

        if (ride is null)
        {
            return Result<VoidResponse>
                .WithError(DomainErrors.RideError.NotFound);
        }

        if (!ride.InProgress)
        {
            Error error = DomainErrors.RideError.InvalidStatusAddPosition;

            logger.LogGenericError(nameof(AddPositionCommandHandler),
                nameof(Handle), error.Message);

            return Result<VoidResponse>
                .WithError(error);
        }

        Result<PositionEntity> entity = PositionEntity.Create(
            ride.Id, request.Latitude, request.Longitude);

        if (entity.IsFailure)
        {
            logger.LogGenericError(
                nameof(AddPositionCommandHandler),
                nameof(Handle), entity.Error!);

            return Result<VoidResponse>
                .FromInvalid(entity.Error!);
        }

        await positionWriteRepository.CreateAsync(
            entity.Value!, cancellationToken);

        logger.LogCreated(nameof(AddPositionCommandHandler),
            nameof(Handle), entity.Value!.Id);

        return Result<VoidResponse>.WithSuccess();
    }
}