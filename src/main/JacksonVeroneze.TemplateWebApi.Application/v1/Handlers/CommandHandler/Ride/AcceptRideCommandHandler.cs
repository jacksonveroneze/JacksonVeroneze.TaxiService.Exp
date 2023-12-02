using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.v1.Commands.Ride;
using JacksonVeroneze.TemplateWebApi.Application.v1.Interfaces.Services.Ride;
using JacksonVeroneze.TemplateWebApi.Application.v1.Interfaces.Services.User;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.Base;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Handlers.CommandHandler.Ride;

public sealed class AcceptRideCommandHandler(
    IGetUserService userService,
    IGetRideService rideService,
    IStatusRideService statusRideService)
    : IRequestHandler<AcceptRideCommand, Result<VoidResponse>>
{
    public async Task<Result<VoidResponse>> Handle(
        AcceptRideCommand request,
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

        Result<UserEntity> driverResult = await userService
            .TryGetUserAsync(request.Body!.DriverId,
                cancellationToken);

        if (driverResult.IsFailure)
        {
            return Result<VoidResponse>
                .FromInvalid(driverResult.Error!);
        }

        Result result = await statusRideService
            .TryAcceptAsync(rideResult.Value!,
                driverResult.Value!, cancellationToken);

        return result.IsSuccess
            ? Result<VoidResponse>.WithSuccess()
            : Result<VoidResponse>.FromInvalid(result.Error!);
    }
}
