using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Messaging;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.Ride;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.User;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Services.Ride;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Services.User;
using JacksonVeroneze.TemplateWebApi.Application.v1.Commands.Ride;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.Base;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.Ride;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;
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
    private readonly IIntegrationEventPublisher _eventPublisher;

    public AcceptRideCommandHandler(
        ILogger<AcceptRideCommandHandler> logger,
        IGetUserService userService,
        IGetRideService rideService,
        IRideWriteRepository writeRepository,
        IIntegrationEventPublisher eventPublisher)
    {
        _logger = logger;
        _userService = userService;
        _rideService = rideService;
        _writeRepository = writeRepository;
        _eventPublisher = eventPublisher;
    }

    public async Task<IResult<VoidResponse>> Handle(
        AcceptRideCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        IResult<UserEntity> driveResult = await _userService
            .TryGetUserAsync(request.DriverId, cancellationToken);

        IResult<RideEntity> rideResult = await _rideService
            .TryGetRideAsync(request.Id, cancellationToken);

        IResult resultValidate = Result
            .FailuresOrSuccess(driveResult, rideResult);

        if (resultValidate.IsFailure)
            return Result<VoidResponse>
                .Invalid(resultValidate.Errors!);

        IResult<VoidResponse> resultStatus = await ProcessAsync(
            rideResult.Value!, driveResult.Value!, cancellationToken);

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
                nameof(ProcessAsync), result.Error!, ride.Id.ToString());

            return Result<VoidResponse>.Invalid(result.Error!);
        }

        await _writeRepository.UpdateAsync(ride, cancellationToken);

        await _eventPublisher.PublishAsync(
            new RideAcceptedDomainEvent(ride.Id),
            cancellationToken);

        _logger.LogProcessed(nameof(AcceptRideCommandHandler),
            nameof(ProcessAsync), ride.Id);

        return Result<VoidResponse>.Success();
    }
}
