using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;
using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;
using JacksonVeroneze.TemplateWebApi.Application.Queries.Client;

namespace JacksonVeroneze.TemplateWebApi.Application.Handlers.QueryHandler.Client;

public class GetClientByIdQueryHandler :
    IRequestHandler<GetClientByIdQuery, BaseResponse>
{
    private readonly ILogger<GetClientByIdQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IClientRepository _repository;

    public GetClientByIdQueryHandler(
        ILogger<GetClientByIdQueryHandler> logger,
        IMapper mapper,
        IClientRepository repository)
    {
        _logger = logger;
        _mapper = mapper;
        _repository = repository;
    }

    public Task<BaseResponse> Handle(
        GetClientByIdQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        throw new NotImplementedException();
    }
}
