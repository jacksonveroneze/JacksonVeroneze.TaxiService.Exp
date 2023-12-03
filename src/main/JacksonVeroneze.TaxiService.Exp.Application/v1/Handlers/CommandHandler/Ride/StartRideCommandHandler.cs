using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Commands.Ride;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Services.Ride;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Base;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Handlers.CommandHandler.Ride;

public sealed class StartRideCommandHandler(
    IGetRideService rideService,
    IStatusRideService statusRideService)
    : IRequestHandler<StartRideCommand, Result<VoidResponse>>
{
    public async Task<Result<VoidResponse>> Handle(
        StartRideCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        Result<RideEntity> rideResult = await rideService
            .TryGetRideAsync(request.Id, cancellationToken);

        if (rideResult.IsFailure)
        {
            return Result<VoidResponse>
                .FromNotFound(rideResult.Error!);
        }

        Result result = await statusRideService
            .TryStartAsync(rideResult.Value!,
                cancellationToken);

        return result.IsSuccess
            ? Result<VoidResponse>.WithSuccess()
            : Result<VoidResponse>.WithError(result.Error!);
    }
}
