using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Application.Extensions;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.User;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.User;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Queries.User;
using JacksonVeroneze.TaxiService.Exp.Domain.Core.Errors;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Handlers.QueryHandler.User;

public sealed class GetUserByIdQueryHandler(
    ILogger<GetUserByIdQueryHandler> logger,
    IMapper mapper,
    IUserReadDistribCachedRepository repository)
    : IRequestHandler<GetUserByIdQuery, Result<GetUserByIdQueryResponse>>
{
    public async Task<Result<GetUserByIdQueryResponse>> Handle(
        GetUserByIdQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        UserEntity? entity = await repository
            .GetByIdAsync(request.Id, cancellationToken);

        if (entity is null)
        {
            return Result<GetUserByIdQueryResponse>
                .FromNotFound(DomainErrors.UserError.NotFound);
        }

        GetUserByIdQueryResponse response =
            mapper.Map<GetUserByIdQueryResponse>(entity);

        logger.LogGetById(nameof(GetUserByIdQueryHandler),
            nameof(Handle), request.Id);

        return Result<GetUserByIdQueryResponse>
            .WithSuccess(response);
    }
}