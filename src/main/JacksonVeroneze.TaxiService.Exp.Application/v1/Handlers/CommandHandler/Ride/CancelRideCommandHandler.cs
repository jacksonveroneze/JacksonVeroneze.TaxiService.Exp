using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Application.Extensions;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Commands.Ride;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.Ride;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Base;
using JacksonVeroneze.TaxiService.Exp.Domain.Core.Errors;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Handlers.CommandHandler.Ride;

public sealed class CancelRideCommandHandler(
    ILogger<CancelRideCommandHandler> logger,
    IRideReadRepository rideReadRepository,
    IRideWriteRepository rideWriteRepository)
    : IRequestHandler<CancelRideCommand, Result<VoidResponse>>
{
    public async Task<Result<VoidResponse>> Handle(
        CancelRideCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        RideEntity? ride = await rideReadRepository
            .GetByIdAsync(request.Id, cancellationToken);

        if (ride is null)
        {
            return Result<VoidResponse>
                .FromNotFound(DomainErrors.RideError.NotFound);
        }

        Result result = ride.Cancel();

        if (result.IsFailure)
        {
            logger.LogGenericError(nameof(CancelRideCommandHandler),
                nameof(Handle), ride.Id, result.Error!);

            return Result<VoidResponse>.WithError(result.Error!);
        }

        await rideWriteRepository.UpdateAsync(ride, cancellationToken);

        logger.LogProcessed(nameof(CancelRideCommandHandler),
            nameof(Handle), ride.Id);

        return Result<VoidResponse>.WithSuccess();
    }
}