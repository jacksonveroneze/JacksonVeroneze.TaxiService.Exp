using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Messaging;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.Ride;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.User;
using JacksonVeroneze.TemplateWebApi.Application.v1.Commands.Ride;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.Base;
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
    private readonly IIntegrationEventPublisher _eventPublisher;

    public RequestRideCommandHandler(
        ILogger<RequestRideCommandHandler> logger,
        IMapper mapper,
        IUserReadRepository userReadRepository,
        IRideReadRepository readRepository,
        IRideWriteRepository writeRepository,
        IIntegrationEventPublisher eventPublisher)
    {
        _logger = logger;
        _mapper = mapper;
        _userReadRepository = userReadRepository;
        _readRepository = readRepository;
        _writeRepository = writeRepository;
        _eventPublisher = eventPublisher;
    }

    public async Task<IResult<RequestRideCommandResponse>> Handle(
        RequestRideCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        // Buscar user
        UserEntity? user = await _userReadRepository
            .GetByIdAsync(request.UserId, cancellationToken);

        // validar se user existe
        if (user is null)
        {
            _logger.LogNotFound(nameof(RequestRideCommandHandler),
                nameof(Handle), request.UserId,
                DomainErrors.User.NotFound);

            return Result<RequestRideCommandResponse>.Invalid(
                DomainErrors.User.NotFound);
        }

        // validar se existe.
        bool existsRide = await _readRepository
            .ExistsByUserAsync(request.UserId, cancellationToken);

        if (existsRide)
        {
            _logger.LogNotFound(nameof(RequestRideCommandHandler),
                nameof(Handle), request.UserId,
                DomainErrors.User.NotFound);

            return Result<RequestRideCommandResponse>.Invalid(
                DomainErrors.User.NotFound);
        }

        // Criar VOs
        IResult<CoordinateValueObject> from = CoordinateValueObject.Create(
            request.LatitudeFrom, request.LongitudeFrom);

        IResult<CoordinateValueObject> to = CoordinateValueObject.Create(
            request.LatitudeTo, request.LongitudeTo);

        // Validar VOs
        IResult resultValidate = Result.FailuresOrSuccess(from, to);

        if (resultValidate.IsFailure)
        {
            _logger.LogGenericError(nameof(RequestRideCommandHandler),
                nameof(Handle), resultValidate.Errors!.Count());

            return Result<RequestRideCommandResponse>
                .Invalid(resultValidate.Errors!);
        }

        // Criar entity
        RideEntity entity = new(user, from.Value, to.Value);

        // Persistir
        await _writeRepository.CreateAsync(entity, cancellationToken);

        // Disparar evento

        // Mapear retorno
        RequestRideCommandResponse response =
            _mapper.Map<RequestRideCommandResponse>(entity);

        // Log
        _logger.LogCreated(nameof(RequestRideCommandHandler),
            nameof(Handle), entity.Id);

        // retorno
        return Result<RequestRideCommandResponse>.Success(response);
    }
}
