using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Application.Extensions;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Commands.Ride;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.Ride;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Base;
using JacksonVeroneze.TaxiService.Exp.Domain.Core.Errors;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Handlers.CommandHandler.Ride;

public sealed class StartRideCommandHandler(
    ILogger<StartRideCommandHandler> logger,
    IRideReadRepository rideReadRepository,
    IRideWriteRepository rideWriteRepository)
    : IRequestHandler<StartRideCommand, Result<VoidResponse>>
{
    public async Task<Result<VoidResponse>> Handle(
        StartRideCommand request,
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

        Result result = ride.Start();

        if (result.IsFailure)
        {
            logger.LogGenericError(nameof(StartRideCommandHandler),
                nameof(Handle), ride.Id, result.Error!);

            return Result<VoidResponse>.WithError(result.Error!);
        }

        await rideWriteRepository.UpdateAsync(ride, cancellationToken);

        logger.LogProcessed(nameof(StartRideCommandHandler),
            nameof(Handle), ride.Id);

        return Result<VoidResponse>.WithSuccess();
    }
}