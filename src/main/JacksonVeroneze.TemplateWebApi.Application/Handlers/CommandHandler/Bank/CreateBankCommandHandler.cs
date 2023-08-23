using JacksonVeroneze.TemplateWebApi.Application.Commands.Bank;
using JacksonVeroneze.TemplateWebApi.Application.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.Bank;
using JacksonVeroneze.TemplateWebApi.Application.Models.Bank;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Primitives;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;

namespace JacksonVeroneze.TemplateWebApi.Application.Handlers.CommandHandler.Bank;

internal sealed  class CreateBankCommandHandler :
    IRequestHandler<CreateBankCommand, IResult<CreateBankCommandResponse>>
{
    private readonly ILogger<CreateBankCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IBankReadRepository _readRepository;
    private readonly IBankWriteRepository _writeRepository;

    public CreateBankCommandHandler(
        ILogger<CreateBankCommandHandler> logger,
        IMapper mapper,
        IBankReadRepository readRepository,
        IBankWriteRepository writeRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _readRepository = readRepository;
        _writeRepository = writeRepository;
    }

    public async Task<IResult<CreateBankCommandResponse>> Handle(
        CreateBankCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        bool any = await _readRepository
            .AnyByNameAsync(request.Name!, cancellationToken);

        if (any)
        {
            _logger.AlreadyExists(nameof(CreateBankCommandHandler),
                nameof(Handle), request.Name!);

            return Result<CreateBankCommandResponse>.Invalid(
                DomainErrors.Bank.DuplicateName);
        }

        BankEntity data = _mapper.Map<BankEntity>(request);

        await _writeRepository.CreateAsync(data, cancellationToken);

        CreateBankCommandResponse response =
            _mapper.Map<CreateBankCommandResponse>(data);

        _logger.LogCreated(nameof(CreateBankCommandHandler),
            nameof(Handle));

        return Result<CreateBankCommandResponse>.Success(response);
    }
}
