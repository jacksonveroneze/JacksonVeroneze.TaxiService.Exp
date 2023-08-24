using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.Client;
using JacksonVeroneze.TemplateWebApi.Application.Models.Account;
using JacksonVeroneze.TemplateWebApi.Application.Queries.Account;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Primitives;

namespace JacksonVeroneze.TemplateWebApi.Application.Handlers.QueryHandler.Account;

internal sealed  class GetAccountByIdQueryHandler :
    IRequestHandler<GetAccountByIdQuery, IResult<GetAccountByIdQueryResponse>>
{
    private readonly ILogger<GetAccountByIdQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IClientReadRepository _repository;

    public GetAccountByIdQueryHandler(
        ILogger<GetAccountByIdQueryHandler> logger,
        IMapper mapper,
        IClientReadRepository repository)
    {
        _logger = logger;
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<IResult<GetAccountByIdQueryResponse>> Handle(
        GetAccountByIdQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        throw new Exception();

        // AccountEntity? data = await _repository
        //     .GetByIdAsync(request.Id, cancellationToken);
        //
        // if (data is null)
        // {
        //     _logger.LogNotFound(nameof(GetAccountByIdQueryHandler),
        //         nameof(Handle), request.Id);
        //
        //     return Result<GetAccountByIdQueryResponse>
        //         .NotFound(DomainErrors.Account.NotFound);
        // }
        //
        // GetAccountByIdQueryResponse response =
        //     _mapper.Map<GetAccountByIdQueryResponse>(data);
        //
        // _logger.LogGetById(nameof(GetAccountByIdQueryHandler),
        //     nameof(Handle), request.Id);
        //
        // return Result<GetAccountByIdQueryResponse>
        //     .Success(response);
    }
}
