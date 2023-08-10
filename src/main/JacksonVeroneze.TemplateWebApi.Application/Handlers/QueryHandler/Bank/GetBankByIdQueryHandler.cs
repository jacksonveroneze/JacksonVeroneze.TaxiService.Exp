using JacksonVeroneze.TemplateWebApi.Application.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;
using JacksonVeroneze.TemplateWebApi.Application.Models.Bank;
using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;
using JacksonVeroneze.TemplateWebApi.Application.Queries.Bank;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;

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

        BankEntity? data = await _repository
            .GetByIdAsync(request.Id, cancellationToken);

        if (data is null)
        {
            _logger.LogNotFound(nameof(GetBankByIdQueryHandler),
                nameof(Handle), request.Id);

            return new BankNotFoundResponse(request.Id.ToString());
        }

        GetBankByIdQueryResponse response =
            _mapper.Map<GetBankByIdQueryResponse>(data);

        _logger.LogGetById(nameof(GetBankByIdQueryHandler),
            nameof(Handle), request.Id);

        return response;
    }
}
