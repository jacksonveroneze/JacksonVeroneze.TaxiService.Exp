using JacksonVeroneze.TemplateWebApi.Application.Commands.Bank;
using JacksonVeroneze.TemplateWebApi.Application.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;
using JacksonVeroneze.TemplateWebApi.Application.Models.Bank;
using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;

namespace JacksonVeroneze.TemplateWebApi.Application.Handlers.CommandHandler.Bank;

public class CreateBankCommandHandler :
    IRequestHandler<CreateBankCommand, BaseResponse>
{
    private readonly ILogger<CreateBankCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IBankWriteRepository _repository;

    public CreateBankCommandHandler(
        ILogger<CreateBankCommandHandler> logger,
        IMapper mapper,
        IBankWriteRepository repository)
    {
        _logger = logger;
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<BaseResponse> Handle(
        CreateBankCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        BankEntity data = _mapper
            .Map<BankEntity>(request);

        // exits

        await _repository.CreateAsync(data, cancellationToken);

        CreateBankCommandResponse response =
            _mapper.Map<CreateBankCommandResponse>(data);

        _logger.LogCreated(nameof(CreateBankCommandHandler),
            nameof(Handle));

        return response;
    }
}
