using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Passengers.Application.Extensions;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.Transaction;
using TaxiService.Modules.Passengers.Application.v1.Models.Transaction;
using TaxiService.Modules.Passengers.Application.v1.Queries.Transaction;
using TaxiService.Modules.Passengers.Domain.Entities;
using TaxiService.Modules.Rides.Application.Core.Errors;

namespace TaxiService.Modules.Passengers.Application.v1.Handlers.QueryHandler.Transaction;

public sealed class GetByRideIdQueryHandler(
    ILogger<GetByRideIdQueryHandler> logger,
    IMapper mapper,
    ITransactionReadRepository transactionReadRepository)
    : IRequestHandler<GetByRideIdQuery, Result<GetByRideIdQueryResponse>>
{
    public async Task<Result<GetByRideIdQueryResponse>> Handle(
        GetByRideIdQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        TransactionEntity? entity = await transactionReadRepository
            .GetByRideIdAsync(request.Id, cancellationToken);

        if (entity is null)
        {
            return Result<GetByRideIdQueryResponse>
                .FromNotFound(DomainErrors.TransactionError.NotFound);
        }

        GetByRideIdQueryResponse response =
            mapper.Map<GetByRideIdQueryResponse>(entity);

        logger.LogGetById(nameof(GetByRideIdQueryHandler),
            nameof(Handle), request.Id);

        return Result<GetByRideIdQueryResponse>
            .WithSuccess(response);
    }
}