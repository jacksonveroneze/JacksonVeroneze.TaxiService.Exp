using JacksonVeroneze.TemplateWebApi.Application.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.Client;
using JacksonVeroneze.TemplateWebApi.Application.Models.Client;
using JacksonVeroneze.TemplateWebApi.Application.Queries.Client;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Primitives;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;

namespace JacksonVeroneze.TemplateWebApi.Application.Handlers.QueryHandler.Client;

internal sealed  class GetClientByIdQueryHandler :
    IRequestHandler<GetClientByIdQuery, IResult<GetClientByIdQueryResponse>>
{
    private readonly ILogger<GetClientByIdQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IClientReadRepository _repository;

    public GetClientByIdQueryHandler(
        ILogger<GetClientByIdQueryHandler> logger,
        IMapper mapper,
        IClientReadRepository repository)
    {
        _logger = logger;
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<IResult<GetClientByIdQueryResponse>> Handle(
        GetClientByIdQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        ClientEntity? data = await _repository
            .GetByIdAsync(request.Id, cancellationToken);

        if (data is null)
        {
            _logger.LogNotFound(nameof(GetClientByIdQueryHandler),
                nameof(Handle), request.Id);

            return Result<GetClientByIdQueryResponse>
                .NotFound(DomainErrors.Client.NotFound);
        }

        GetClientByIdQueryResponse response =
            _mapper.Map<GetClientByIdQueryResponse>(data);

        _logger.LogGetById(nameof(GetClientByIdQueryHandler),
            nameof(Handle), request.Id);

        return Result<GetClientByIdQueryResponse>
            .Success(response);
    }
}
