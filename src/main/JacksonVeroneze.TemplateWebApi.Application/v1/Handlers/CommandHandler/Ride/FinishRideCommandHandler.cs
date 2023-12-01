using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.v1.Commands.Ride;
using JacksonVeroneze.TemplateWebApi.Application.v1.Interfaces.Services.Ride;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.Base;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Handlers.CommandHandler.Ride;

public sealed class FinishRideCommandHandler(
    IGetRideService rideService,
    IStatusRideService statusRideService)
    : IRequestHandler<FinishRideCommand, IResult<VoidResponse>>
{
    public async Task<IResult<VoidResponse>> Handle(
        FinishRideCommand request,
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

        IResult result = await statusRideService
            .TryFinishAsync(rideResult.Value!,
                cancellationToken);

        return result.IsSuccess
            ? Result<VoidResponse>.Success()
            : Result<VoidResponse>.Invalid(result.Error!);
    }
}
