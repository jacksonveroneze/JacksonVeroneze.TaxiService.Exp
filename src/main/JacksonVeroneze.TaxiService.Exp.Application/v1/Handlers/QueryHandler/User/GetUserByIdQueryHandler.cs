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
    IUserReadRepository userReadRepository)
    : IRequestHandler<GetUserByIdQuery, Result<GetUserByIdQueryResponse>>
{
    public async Task<Result<GetUserByIdQueryResponse>> Handle(
        GetUserByIdQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        (UserEntity? Entity, Error? Error) userResult =
            await GetUserByIdAsync(request.Id, cancellationToken);

        if (userResult.Entity is null)
        {
            return Result<GetUserByIdQueryResponse>
                .FromNotFound(userResult.Error!);
        }

        GetUserByIdQueryResponse response =
            mapper.Map<GetUserByIdQueryResponse>(userResult.Entity);

        logger.LogGetById(nameof(GetUserByIdQueryHandler),
            nameof(Handle), request.Id);

        return Result<GetUserByIdQueryResponse>
            .WithSuccess(response);
    }

    private async Task<(UserEntity?, Error?)> GetUserByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        UserEntity? entity = await userReadRepository
            .GetByIdAsync(id, cancellationToken);

        if (entity is not null)
        {
            return (entity, null);
        }

        Error error = DomainErrors.UserError.NotFound;

        logger.LogNotFound(
            nameof(GetUserByIdQueryHandler),
            nameof(GetUserByIdAsync), id, error);

        return (null, error);
    }
}