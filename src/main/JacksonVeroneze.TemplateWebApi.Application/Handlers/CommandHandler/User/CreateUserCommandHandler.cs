using JacksonVeroneze.TemplateWebApi.Application.Commands.User;
using JacksonVeroneze.TemplateWebApi.Application.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.User;
using JacksonVeroneze.TemplateWebApi.Application.Models.User;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Primitives;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;

namespace JacksonVeroneze.TemplateWebApi.Application.Handlers.CommandHandler.User;

internal sealed class CreateUserCommandHandler :
    IRequestHandler<CreateUserCommand, IResult<CreateUserCommandResponse>>
{
    private readonly ILogger<CreateUserCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IUserReadRepository _readRepository;
    private readonly IUserWriteRepository _writeRepository;

    public CreateUserCommandHandler(
        ILogger<CreateUserCommandHandler> logger,
        IMapper mapper,
        IUserReadRepository readRepository,
        IUserWriteRepository writeRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _readRepository = readRepository;
        _writeRepository = writeRepository;
    }

    public async Task<IResult<CreateUserCommandResponse>> Handle(
        CreateUserCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        bool existsByName = await _readRepository
            .ExistsByNameAsync(request.Name!, cancellationToken);

        if (existsByName)
        {
            _logger.AlreadyExists(nameof(CreateUserCommandHandler),
                nameof(Handle), DomainErrors.User.DuplicateName, request.Name!);

            return Result<CreateUserCommandResponse>.Invalid(
                DomainErrors.User.DuplicateName);
        }

        // TODO - Validar DomainObjects
        // TODO - Criar user manualmente

        UserEntity entity = _mapper.Map<UserEntity>(request);

        await _writeRepository.CreateAsync(entity, cancellationToken);

        CreateUserCommandResponse response =
            _mapper.Map<CreateUserCommandResponse>(entity);

        _logger.LogCreated(nameof(CreateUserCommandHandler),
            nameof(Handle), entity.Id);

        return Result<CreateUserCommandResponse>.Success(response);
    }
}
