using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.User;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.User;
using JacksonVeroneze.TemplateWebApi.Application.v1.Queries.User;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Handlers.QueryHandler.User;

public sealed class GetUserByIdQueryHandler :
    IRequestHandler<GetUserByIdQuery, IResult<GetUserByIdQueryResponse>>
{
    private readonly ILogger<GetUserByIdQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IUserReadRepository _repository;

    public GetUserByIdQueryHandler(
        ILogger<GetUserByIdQueryHandler> logger,
        IMapper mapper,
        IUserReadRepository repository)
    {
        _logger = logger;
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<IResult<GetUserByIdQueryResponse>> Handle(
        GetUserByIdQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        UserEntity? entity = await _repository
            .GetByIdAsync(request.Id, cancellationToken);

        if (entity is null)
        {
            return Result<GetUserByIdQueryResponse>
                .NotFound(DomainErrors.User.NotFound);
        }

        GetUserByIdQueryResponse response =
            _mapper.Map<GetUserByIdQueryResponse>(entity);

        _logger.LogGetById(nameof(GetUserByIdQueryHandler),
            nameof(Handle), request.Id);

        return Result<GetUserByIdQueryResponse>
            .Success(response);
    }
}
