using JacksonVeroneze.TemplateWebApi.Application.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;
using JacksonVeroneze.TemplateWebApi.Application.Models.Bank;
using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;
using JacksonVeroneze.TemplateWebApi.Application.Queries.Bank;

namespace JacksonVeroneze.TemplateWebApi.Application.Handlers.QueryHandler.Bank;

public class GetBankByIdQueryHandler :
    IRequestHandler<GetBankByIdQuery, BaseResponse>
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

    public async Task<BaseResponse> Handle(
        GetBankByIdQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        Domain.Entities.Bank? result = await _repository
            .GetByIdAsync(request.Id, cancellationToken);

        if (result is null)
        {
            _logger.LogNotFound(nameof(GetBankByIdQueryHandler),
                nameof(Handle));

            return new BankNotFoundResponse(request.Id.ToString());
        }

        GetBankByIdQueryResponse response =
            _mapper.Map<GetBankByIdQueryResponse>(result);

        _logger.LogGetById(nameof(GetBankByIdQueryHandler),
            nameof(Handle), request.Id);

        return response;
    }
}
