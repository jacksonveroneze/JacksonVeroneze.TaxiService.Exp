using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Messaging;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.Ride;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Services.Ride;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Services.User;
using JacksonVeroneze.TemplateWebApi.Application.v1.Commands.Ride;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.Base;
using JacksonVeroneze.TemplateWebApi.Domain.DomainEvents.Ride;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Handlers.CommandHandler.Ride;

public sealed class AcceptRideCommandHandler :
    IRequestHandler<AcceptRideCommand, IResult<VoidResponse>>
{
    private readonly ILogger<AcceptRideCommandHandler> _logger;
    private readonly IGetUserService _userService;
    private readonly IGetRideService _rideService;
    private readonly IRideWriteRepository _writeRepository;

    public AcceptRideCommandHandler(
        ILogger<AcceptRideCommandHandler> logger,
        IGetUserService userService,
        IGetRideService rideService,
        IRideWriteRepository writeRepository)
    {
        _logger = logger;
        _userService = userService;
        _rideService = rideService;
        _writeRepository = writeRepository;
    }

    public async Task<IResult<VoidResponse>> Handle(
        AcceptRideCommand request,
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

        IResult<UserEntity> driverResult = await _userService
            .TryGetUserAsync(request.Body!.DriverId, cancellationToken);

        if (driverResult.IsFailure)
        {
            return Result<VoidResponse>
                .Invalid(driverResult.Error!);
        }

        IResult<VoidResponse> resultStatus = await ProcessAsync(
            rideResult.Value!, driverResult.Value!, cancellationToken);

        return resultStatus;
    }

    private async Task<IResult<VoidResponse>> ProcessAsync(
        RideEntity ride, UserEntity drive,
        CancellationToken cancellationToken)
    {
        IResult result = ride.Accept(drive);

        if (result.IsFailure)
        {
            _logger.LogGenericError(nameof(AcceptRideCommandHandler),
                nameof(ProcessAsync), ride.Id, result.Error!);

            return Result<VoidResponse>.Invalid(result.Error!);
        }

        await _writeRepository.UpdateAsync(ride, cancellationToken);

        _logger.LogProcessed(nameof(AcceptRideCommandHandler),
            nameof(ProcessAsync), ride.Id);

        return Result<VoidResponse>.Success();
    }
}
