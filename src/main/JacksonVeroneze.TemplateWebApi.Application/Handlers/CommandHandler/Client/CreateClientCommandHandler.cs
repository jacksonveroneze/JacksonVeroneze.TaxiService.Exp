using JacksonVeroneze.TemplateWebApi.Application.Commands.Client;
using JacksonVeroneze.TemplateWebApi.Application.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.Client;
using JacksonVeroneze.TemplateWebApi.Application.Models.Client;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Primitives;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;

namespace JacksonVeroneze.TemplateWebApi.Application.Handlers.CommandHandler.Client;

internal sealed  class CreateClientCommandHandler :
    IRequestHandler<CreateClientCommand, IResult<CreateClientCommandResponse>>
{
    private readonly ILogger<CreateClientCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IClientReadRepository _readRepository;
    private readonly IClientWriteRepository _writeRepository;

    public CreateClientCommandHandler(
        ILogger<CreateClientCommandHandler> logger,
        IMapper mapper,
        IClientReadRepository readRepository,
        IClientWriteRepository writeRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _readRepository = readRepository;
        _writeRepository = writeRepository;
    }

    public async Task<IResult<CreateClientCommandResponse>> Handle(
        CreateClientCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        bool any = await _readRepository
            .AnyByNameAsync(request.Name!, cancellationToken);

        if (any)
        {
            _logger.AlreadyExists(nameof(CreateClientCommandHandler),
                nameof(Handle), request.Name!);

            return Result<CreateClientCommandResponse>.Invalid(
                DomainErrors.Client.DuplicateDocument);
        }

        ClientEntity data = _mapper.Map<ClientEntity>(request);

        await _writeRepository.CreateAsync(data, cancellationToken);

        CreateClientCommandResponse response =
            _mapper.Map<CreateClientCommandResponse>(data);

        _logger.LogCreated(nameof(CreateClientCommandHandler),
            nameof(Handle));

        return Result<CreateClientCommandResponse>.Success(response);
    }
}
