using JacksonVeroneze.TemplateWebApi.Application.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.Bank;
using JacksonVeroneze.TemplateWebApi.Application.Models.Bank;
using JacksonVeroneze.TemplateWebApi.Application.Queries.Bank;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Primitives;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;

namespace JacksonVeroneze.TemplateWebApi.Application.Handlers.QueryHandler.Bank;

internal sealed  class GetBankByIdQueryHandler :
    IRequestHandler<GetBankByIdQuery, IResult<GetBankByIdQueryResponse>>
{
    private readonly ILogger<GetBankByIdQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IBankReadRepository _repository;

    public GetBankByIdQueryHandler(
        ILogger<GetBankByIdQueryHandler> logger,
        IMapper mapper,
        IBankReadRepository repository)
    {
        _logger = logger;
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<IResult<GetBankByIdQueryResponse>> Handle(
        GetBankByIdQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        BankEntity? data = await _repository
            .GetByIdAsync(request.Id, cancellationToken);

        if (data is null)
        {
            _logger.LogNotFound(nameof(GetBankByIdQueryHandler),
                nameof(Handle), request.Id);

            return Result<GetBankByIdQueryResponse>
                .NotFound(DomainErrors.Bank.NotFound);
        }

        GetBankByIdQueryResponse response =
            _mapper.Map<GetBankByIdQueryResponse>(data);

        _logger.LogGetById(nameof(GetBankByIdQueryHandler),
            nameof(Handle), request.Id);

        return Result<GetBankByIdQueryResponse>
            .Success(response);
    }
}
