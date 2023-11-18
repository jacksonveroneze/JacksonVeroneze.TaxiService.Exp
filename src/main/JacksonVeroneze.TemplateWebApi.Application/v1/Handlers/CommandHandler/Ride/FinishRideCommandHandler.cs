using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Services.Ride;
using JacksonVeroneze.TemplateWebApi.Application.v1.Commands.Ride;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.Base;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Handlers.CommandHandler.Ride;

public sealed class FinishRideCommandHandler :
    IRequestHandler<FinishRideCommand, IResult<VoidResponse>>
{
    private readonly IGetRideService _rideService;
    private readonly IStatusRideService _statusRideService;

    public FinishRideCommandHandler(
        ILogger<FinishRideCommandHandler> logger,
        IGetRideService rideService,
        IStatusRideService statusRideService)
    {
        _rideService = rideService;
        _statusRideService = statusRideService;
    }

    public async Task<IResult<VoidResponse>> Handle(
        FinishRideCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        IResult<RideEntity> rideResult = await _rideService
            .TryGetRideAsync(request.Id, cancellationToken);

        if (rideResult.IsFailure)
        {
            return Result<VoidResponse>
                .NotFound(rideResult.Error!);
        }

        IResult result = await _statusRideService
            .TryFinishAsync(rideResult.Value!,
                cancellationToken);

        return result.IsSuccess
            ? Result<VoidResponse>.Success()
            : Result<VoidResponse>.Invalid(result.Error!);
    }
}
