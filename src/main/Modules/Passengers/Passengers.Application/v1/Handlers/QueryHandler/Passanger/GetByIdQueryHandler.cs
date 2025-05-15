using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Common.BaseApplication.Interfaces.Mapper;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.Passanger;
using TaxiService.Modules.Passengers.Application.v1.Models.Passanger;
using TaxiService.Modules.Passengers.Application.v1.Queries.Passanger;
using TaxiService.Modules.Passengers.Domain.Core.Errors;
using TaxiService.Modules.Passengers.Domain.Entities;

namespace TaxiService.Modules.Passengers.Application.v1.Handlers.QueryHandler.Passanger;

public sealed class GetByIdQueryHandler(
    ILogger<GetByIdQueryHandler> logger,
    IDataMapper mapper,
    IPassangerReadRepository passangerReadRepository)
    : IRequestHandler<GetByIdQuery, Result<GetByIdQueryResponse>>
{
    public async Task<Result<GetByIdQueryResponse>> Handle(
        GetByIdQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        Result<PassangerEntity> resultEntity = await GetByIdAsync(
            request.Id, cancellationToken);

        if (resultEntity.IsFailure)
        {
            return Result<GetByIdQueryResponse>
                .FromNotFound(resultEntity.FirstError!);
        }

        GetByIdQueryResponse response =
            mapper.Map<GetByIdQueryResponse>(resultEntity.Value);

        logger.LogGetById(nameof(GetByIdQueryHandler),
            nameof(Handle), request.Id);

        return Result<GetByIdQueryResponse>
            .WithSuccess(response);
    }

    private async Task<Result<PassangerEntity>> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        PassangerEntity? entity = await passangerReadRepository
            .GetByIdAsync(id, cancellationToken);

        if (entity is not null)
        {
            return Result<PassangerEntity>.WithSuccess(entity);
        }

        Error error = DomainErrors.PassangerError.NotFound;

        logger.LogNotFound(
            nameof(GetByIdQueryHandler),
            nameof(GetByIdAsync), id, error);

        return Result<PassangerEntity>.FromNotFound(error);
    }
}
