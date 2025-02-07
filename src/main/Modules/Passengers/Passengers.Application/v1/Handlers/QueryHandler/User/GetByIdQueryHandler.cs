using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.User;
using TaxiService.Modules.Passengers.Application.v1.Models.User;
using TaxiService.Modules.Passengers.Application.v1.Queries.User;
using TaxiService.Modules.Passengers.Domain.Core.Errors;
using TaxiService.Modules.Passengers.Domain.Entities;

namespace TaxiService.Modules.Passengers.Application.v1.Handlers.QueryHandler.User;

public sealed class GetByIdQueryHandler(
    ILogger<GetByIdQueryHandler> logger,
    IMapper mapper,
    IUserReadRepository userReadRepository)
    : IRequestHandler<GetByIdQuery, Result<GetByIdQueryResponse>>
{
    public async Task<Result<GetByIdQueryResponse>> Handle(
        GetByIdQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        Result<UserEntity> resultEntity = await GetUserByIdAsync(
            request.Id, cancellationToken);

        if (resultEntity.IsFailure)
        {
            return Result<GetByIdQueryResponse>
                .FromNotFound(resultEntity.Error!);
        }

        GetByIdQueryResponse response =
            mapper.Map<GetByIdQueryResponse>(resultEntity.Value);

        logger.LogGetById(nameof(GetByIdQueryHandler),
            nameof(Handle), request.Id);

        return Result<GetByIdQueryResponse>
            .WithSuccess(response);
    }

    private async Task<Result<UserEntity>> GetUserByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        UserEntity? entity = await userReadRepository
            .GetByIdAsync(id, cancellationToken);

        if (entity is not null)
        {
            return Result<UserEntity>.WithSuccess(entity);
        }

        Error error = DomainErrors.UserError.NotFound;

        logger.LogNotFound(
            nameof(GetByIdQueryHandler),
            nameof(GetUserByIdAsync), id, error);

        return Result<UserEntity>.FromNotFound(error);
    }
}