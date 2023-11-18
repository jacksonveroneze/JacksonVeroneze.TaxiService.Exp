using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.Ride;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Services.Ride;
using JacksonVeroneze.TemplateWebApi.Application.v1.Commands.Ride;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.Base;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Handlers.CommandHandler.Ride;

public sealed class FinishRideCommandHandler :
    IRequestHandler<FinishRideCommand, IResult<VoidResponse>>
{
    private readonly ILogger<FinishRideCommandHandler> _logger;
    private readonly IGetRideService _rideService;
    private readonly IRideWriteRepository _writeRepository;

    public FinishRideCommandHandler(
        ILogger<FinishRideCommandHandler> logger,
        IGetRideService rideService,
        IRideWriteRepository writeRepository)
    {
        _logger = logger;
        _rideService = rideService;
        _writeRepository = writeRepository;
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

        IResult<VoidResponse> resultStatus = await ProcessAsync(
            rideResult.Value!, cancellationToken);

        return resultStatus;
    }

    private async Task<IResult<VoidResponse>> ProcessAsync(
        RideEntity ride,
        CancellationToken cancellationToken)
    {
        IResult result = ride.Finish();

        if (result.IsFailure)
        {
            _logger.LogGenericError(nameof(FinishRideCommandHandler),
                nameof(ProcessAsync), ride.Id, result.Error!);

            return Result<VoidResponse>.Invalid(result.Error!);
        }

        await _writeRepository.UpdateAsync(ride, cancellationToken);

        _logger.LogProcessed(nameof(FinishRideCommandHandler),
            nameof(ProcessAsync), ride.Id);

        return Result<VoidResponse>.Success();
    }
}
