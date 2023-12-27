using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Application.Extensions;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.Transaction;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Transaction;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Queries.Transaction;
using JacksonVeroneze.TaxiService.Exp.Domain.Core.Errors;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Handlers.QueryHandler.Transaction;

public sealed class GetByRideIdQueryHandler(
    ILogger<GetByRideIdQueryHandler> logger,
    IMapper mapper,
    ITransactionReadRepository repository)
    : IRequestHandler<GetByRideIdQuery, Result<GetByRideIdQueryResponse>>
{
    public async Task<Result<GetByRideIdQueryResponse>> Handle(
        GetByRideIdQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        TransactionEntity? entity = await repository
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