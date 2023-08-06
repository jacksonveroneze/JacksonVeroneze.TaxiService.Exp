using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;
using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;
using JacksonVeroneze.TemplateWebApi.Application.Queries.Bank;

namespace JacksonVeroneze.TemplateWebApi.Application.Handlers.QueryHandler.Bank;

public class GetBankPagedQueryHandler :
    IRequestHandler<GetBankPagedQuery, BaseResponse>
{
    private readonly ILogger<GetBankPagedQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IBankRepository _repository;

    public GetBankPagedQueryHandler(
        ILogger<GetBankPagedQueryHandler> logger,
        IMapper mapper,
        IBankRepository repository)
    {
        _logger = logger;
        _mapper = mapper;
        _repository = repository;
    }

    public Task<BaseResponse> Handle(
        GetBankPagedQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        throw new NotImplementedException();
    }
}
