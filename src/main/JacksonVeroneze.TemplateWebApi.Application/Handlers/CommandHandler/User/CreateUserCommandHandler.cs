using JacksonVeroneze.TemplateWebApi.Application.Commands.User;
using JacksonVeroneze.TemplateWebApi.Application.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.User;
using JacksonVeroneze.TemplateWebApi.Application.Models.User;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Primitives;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;

namespace JacksonVeroneze.TemplateWebApi.Application.Handlers.CommandHandler.User;

internal sealed  class CreateUserCommandHandler :
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

        bool any = await _readRepository
            .AnyByNameAsync(request.Name!, cancellationToken);

        if (any)
        {
            _logger.AlreadyExists(nameof(CreateUserCommandHandler),
                nameof(Handle), request.Name!);

            return Result<CreateUserCommandResponse>.Invalid(
                DomainErrors.User.DuplicateName);
        }

        UserEntity data = _mapper.Map<UserEntity>(request);

        await _writeRepository.CreateAsync(data, cancellationToken);

        CreateUserCommandResponse response =
            _mapper.Map<CreateUserCommandResponse>(data);

        _logger.LogCreated(nameof(CreateUserCommandHandler),
            nameof(Handle));

        return Result<CreateUserCommandResponse>.Success(response);
    }
}
