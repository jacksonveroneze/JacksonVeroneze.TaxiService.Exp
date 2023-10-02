using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.User;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.User.Email;
using JacksonVeroneze.TemplateWebApi.Application.v1.Queries.User.Email;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Handlers.QueryHandler.User.Email;

public sealed class GetAllEmailsByUserIdQueryHandler :
    IRequestHandler<GetAllEmailsByUserIdQuery, IResult<GetAllEmailsByUserIdQueryResponse>>
{
    private readonly ILogger<GetAllEmailsByUserIdQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IUserReadRepository _repository;

    public GetAllEmailsByUserIdQueryHandler(
        ILogger<GetAllEmailsByUserIdQueryHandler> logger,
        IMapper mapper,
        IUserReadRepository repository)
    {
        _logger = logger;
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<IResult<GetAllEmailsByUserIdQueryResponse>> Handle(
        GetAllEmailsByUserIdQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        UserEntity? entity = await _repository
            .GetByIdAsync(request.Id, cancellationToken);

        if (entity is null)
        {
            _logger.LogNotFound(nameof(GetAllEmailsByUserIdQueryHandler),
                nameof(Handle), request.Id, DomainErrors.User.NotFound);

            return Result<GetAllEmailsByUserIdQueryResponse>.NotFound(
                DomainErrors.User.NotFound);
        }

        IReadOnlyCollection<EmailEntity> emails = entity.Emails;

        GetAllEmailsByUserIdQueryResponse response =
            _mapper.Map<GetAllEmailsByUserIdQueryResponse>(emails);

        _logger.LogGetPaged(nameof(GetAllEmailsByUserIdQueryHandler),
            nameof(Handle), 1, emails.Count, emails.Count);

        return Result<GetAllEmailsByUserIdQueryResponse>.Success(response);
    }
}
