using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;
using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;
using JacksonVeroneze.TemplateWebApi.Application.Queries.Bank;

namespace JacksonVeroneze.TemplateWebApi.Application.Handlers.QueryHandler.Bank;

public class GetBankByIdQueryHandler :
    IRequestHandler<GetBankByIdQuery, BaseResponse>
{
    private readonly ILogger<GetBankByIdQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IBankRepository _repository;

    public GetBankByIdQueryHandler(
        ILogger<GetBankByIdQueryHandler> logger,
        IMapper mapper,
        IBankRepository repository)
    {
        _logger = logger;
        _mapper = mapper;
        _repository = repository;
    }

    public Task<BaseResponse> Handle(
        GetBankByIdQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        throw new NotImplementedException();
    }
}
