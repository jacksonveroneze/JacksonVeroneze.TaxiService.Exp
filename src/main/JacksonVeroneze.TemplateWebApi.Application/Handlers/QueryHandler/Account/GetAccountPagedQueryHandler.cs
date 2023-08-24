using JacksonVeroneze.TemplateWebApi.Application.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.Client;
using JacksonVeroneze.TemplateWebApi.Application.Models.Account;
using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;
using JacksonVeroneze.TemplateWebApi.Application.Queries.Account;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Primitives;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;

namespace JacksonVeroneze.TemplateWebApi.Application.Handlers.QueryHandler.Account;

internal sealed  class GetAccountPagedQueryHandler :
    IRequestHandler<GetAccountPagedQuery, IResult<BaseResponse>>
{
    private readonly ILogger<GetAccountPagedQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IClientReadRepository _repository;

    public GetAccountPagedQueryHandler(
        ILogger<GetAccountPagedQueryHandler> logger,
        IMapper mapper,
        IClientReadRepository repository)
    {
        _logger = logger;
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<IResult<BaseResponse>> Handle(
        GetAccountPagedQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        ClientEntity? data = await _repository
            .GetByIdAsync(request.ClientId, cancellationToken);

        if (data is null)
        {
            _logger.LogNotFound(nameof(GetAccountPagedQueryHandler),
                nameof(Handle), request.ClientId);

            return Result<BaseResponse>
                .NotFound(DomainErrors.Client.NotFound);
        }

        GetAccountPagedQueryResponse response =
            _mapper.Map<GetAccountPagedQueryResponse>(data.Accounts);

        // _logger.LogGetPaged(nameof(GetAccountPagedQueryHandler),
        //     nameof(Handle),
        //     data.Pagination.Page,
        //     data.Pagination.PageSize,
        //     data.Pagination.TotalElements);

        return Result<BaseResponse>.Success(response);
    }
}
