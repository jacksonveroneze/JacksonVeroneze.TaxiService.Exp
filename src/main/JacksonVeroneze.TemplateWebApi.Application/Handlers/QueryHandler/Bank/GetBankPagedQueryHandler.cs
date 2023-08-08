using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TemplateWebApi.Application.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;
using JacksonVeroneze.TemplateWebApi.Application.Models.Bank;
using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;
using JacksonVeroneze.TemplateWebApi.Application.Queries.Bank;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;

namespace JacksonVeroneze.TemplateWebApi.Application.Handlers.QueryHandler.Bank;

public class GetBankPagedQueryHandler :
    IRequestHandler<GetBankPagedQuery, BaseResponse>
{
    private readonly ILogger<GetBankPagedQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IBankReadRepository _repository;

    public GetBankPagedQueryHandler(
        ILogger<GetBankPagedQueryHandler> logger,
        IMapper mapper,
        IBankReadRepository repository)
    {
        _logger = logger;
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<BaseResponse> Handle(
        GetBankPagedQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        BankPagedFilter filter = _mapper
            .Map<BankPagedFilter>(request);

        Page<Domain.Entities.Bank> result = await _repository
            .GetPagedAsync(filter, cancellationToken);

        GetBankPagedQueryResponse response =
            _mapper.Map<GetBankPagedQueryResponse>(result);

        _logger.LogGetPaged(nameof(GetBankPagedQueryHandler),
            nameof(Handle), result.Pagination.TotalElements);

        return response;
    }
}
