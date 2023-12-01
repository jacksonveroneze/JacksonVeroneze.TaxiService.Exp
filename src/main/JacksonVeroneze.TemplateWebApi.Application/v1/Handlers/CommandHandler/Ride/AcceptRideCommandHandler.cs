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
    : IRequestHandler<AcceptRideCommand, IResult<VoidResponse>>
{
    public async Task<IResult<VoidResponse>> Handle(
        AcceptRideCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        IResult<RideEntity> rideResult = await rideService
            .TryGetRideAsync(request.Id, cancellationToken);

        if (rideResult.IsFailure)
        {
            return Result<VoidResponse>
                .NotFound(rideResult.Error!);
        }

        IResult<UserEntity> driverResult = await userService
            .TryGetUserAsync(request.Body!.DriverId,
                cancellationToken);

        if (driverResult.IsFailure)
        {
            return Result<VoidResponse>
                .Invalid(driverResult.Error!);
        }

        IResult result = await statusRideService
            .TryAcceptAsync(rideResult.Value!,
                driverResult.Value!, cancellationToken);

        return result.IsSuccess
            ? Result<VoidResponse>.Success()
            : Result<VoidResponse>.Invalid(result.Error!);
    }
}
