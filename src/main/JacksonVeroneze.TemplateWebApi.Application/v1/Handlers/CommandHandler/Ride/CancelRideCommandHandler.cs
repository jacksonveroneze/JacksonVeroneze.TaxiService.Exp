using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.v1.Commands.Ride;
using JacksonVeroneze.TemplateWebApi.Application.v1.Interfaces.Services.Ride;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.Base;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Handlers.CommandHandler.Ride;

public sealed class CancelRideCommandHandler(
    IGetRideService rideService,
    IStatusRideService statusRideService)
    : IRequestHandler<CancelRideCommand, Result<VoidResponse>>
{
    public async Task<Result<VoidResponse>> Handle(
        CancelRideCommand request,
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
            .TryCancelAsync(rideResult.Value!,
                cancellationToken);

        return result.IsSuccess
            ? Result<VoidResponse>.WithSuccess()
            : Result<VoidResponse>.FromInvalid(result.Error!);
    }
}
