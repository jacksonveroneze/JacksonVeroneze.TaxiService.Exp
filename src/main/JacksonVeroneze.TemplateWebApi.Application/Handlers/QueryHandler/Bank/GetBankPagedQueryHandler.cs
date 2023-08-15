using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TemplateWebApi.Application.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;
using JacksonVeroneze.TemplateWebApi.Application.Models.Bank;
using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;
using JacksonVeroneze.TemplateWebApi.Application.Primitives;
using JacksonVeroneze.TemplateWebApi.Application.Queries.Bank;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;

namespace JacksonVeroneze.TemplateWebApi.Application.Handlers.QueryHandler.Bank;

public class GetBankPagedQueryHandler :
    IRequestHandler<GetBankPagedQuery, IResult<BaseResponse>>
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

    public async Task<IResult<BaseResponse>> Handle(
        GetBankPagedQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        BankPagedFilter filter = _mapper
            .Map<BankPagedFilter>(request);

        Page<BankEntity> data = await _repository
            .GetPagedAsync(filter, cancellationToken);

        GetBankPagedQueryResponse response =
            _mapper.Map<GetBankPagedQueryResponse>(data);

        _logger.LogGetPaged(nameof(GetBankPagedQueryHandler),
            nameof(Handle),
            data.Pagination.Page,
            data.Pagination.PageSize,
            data.Pagination.TotalElements);

        return Result<BaseResponse>.Success(response);
    }
}
