using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.v1.Interfaces.Repositories.User;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.User;
using JacksonVeroneze.TemplateWebApi.Application.v1.Queries.User;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Handlers.QueryHandler.User;

public sealed class GetUserByIdQueryHandler(
    ILogger<GetUserByIdQueryHandler> logger,
    IMapper mapper,
    IUserReadRepository repository)
    : IRequestHandler<GetUserByIdQuery, IResult<GetUserByIdQueryResponse>>
{
    public async Task<IResult<GetUserByIdQueryResponse>> Handle(
        GetUserByIdQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        UserEntity? entity = await repository
            .GetByIdAsync(request.Id, cancellationToken);

        if (entity is null)
        {
            return Result<GetUserByIdQueryResponse>
                .NotFound(DomainErrors.User.NotFound);
        }

        GetUserByIdQueryResponse response =
            mapper.Map<GetUserByIdQueryResponse>(entity);

        logger.LogGetById(nameof(GetUserByIdQueryHandler),
            nameof(Handle), request.Id);

        return Result<GetUserByIdQueryResponse>
            .Success(response);
    }
}
