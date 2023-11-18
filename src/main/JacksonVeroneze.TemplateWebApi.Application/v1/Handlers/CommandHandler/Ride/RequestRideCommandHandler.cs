using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.Ride;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.User;
using JacksonVeroneze.TemplateWebApi.Application.v1.Commands.Ride;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.Ride;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.ValueObjects;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Handlers.CommandHandler.Ride;

public sealed class RequestRideCommandHandler :
    IRequestHandler<RequestRideCommand, IResult<RequestRideCommandResponse>>
{
    private readonly ILogger<RequestRideCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IUserReadRepository _userReadRepository;
    private readonly IRideReadRepository _readRepository;
    private readonly IRideWriteRepository _writeRepository;

    public RequestRideCommandHandler(
        ILogger<RequestRideCommandHandler> logger,
        IMapper mapper,
        IUserReadRepository userReadRepository,
        IRideReadRepository readRepository,
        IRideWriteRepository writeRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _userReadRepository = userReadRepository;
        _readRepository = readRepository;
        _writeRepository = writeRepository;
    }

    public async Task<IResult<RequestRideCommandResponse>> Handle(
        RequestRideCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        UserEntity? user = await _userReadRepository
            .GetByIdAsync(request.UserId, cancellationToken);

        Task<bool> existsRideByUserTask = _readRepository
            .ExistsByUserAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            return Result<RequestRideCommandResponse>.Invalid(
                DomainErrors.User.NotFound);
        }

        bool existsRideByUser = await existsRideByUserTask;

        if (existsRideByUser)
        {
            _logger.LogAlreadyExists(nameof(RequestRideCommandHandler),
                nameof(Handle), user.Id, DomainErrors.Ride.AlreadyByUser);

            return Result<RequestRideCommandResponse>.Invalid(
                DomainErrors.Ride.AlreadyByUser);
        }

        IResult<CoordinateValueObject> from = CoordinateValueObject.Create(
            request.LatitudeFrom, request.LongitudeFrom);

        IResult<CoordinateValueObject> to = CoordinateValueObject.Create(
            request.LatitudeTo, request.LongitudeTo);

        IResult resultValidate = Result.FailuresOrSuccess(from, to);

        if (resultValidate.IsFailure)
        {
            _logger.LogGenericError(nameof(RequestRideCommandHandler),
                nameof(Handle), resultValidate.Errors!.Count());

            return Result<RequestRideCommandResponse>
                .Invalid(resultValidate.Errors!);
        }

        RideEntity entity = new(user, from.Value, to.Value);

        await _writeRepository.CreateAsync(
            entity, cancellationToken);

        RequestRideCommandResponse response =
            _mapper.Map<RequestRideCommandResponse>(entity);

        _logger.LogCreated(nameof(RequestRideCommandHandler),
            nameof(Handle), entity.Id);

        return Result<RequestRideCommandResponse>.Success(response);
    }
}
