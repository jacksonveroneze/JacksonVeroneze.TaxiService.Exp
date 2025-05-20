using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Common.BaseApplication.Interfaces.Mapper;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.Passenger;
using TaxiService.Modules.Passengers.Application.v1.Models.Passenger;
using TaxiService.Modules.Passengers.Application.v1.Queries.Passenger;
using TaxiService.Modules.Passengers.Domain.Core.Errors;
using TaxiService.Modules.Passengers.Domain.Entities;

namespace TaxiService.Modules.Passengers.Application.v1.Handlers.QueryHandler.Passenger;

public sealed class GetByIdQueryHandler(
    ILogger<GetByIdQueryHandler> logger,
    IDataMapper mapper,
    IPassengerReadRepository passengerReadRepository)
    : IRequestHandler<GetByIdQuery, Result<GetByIdQueryResponse>>
{
    public async Task<Result<GetByIdQueryResponse>> Handle(
        GetByIdQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        Result<PassengerEntity> resultEntity = await GetByIdAsync(
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

    private async Task<Result<PassengerEntity>> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        PassengerEntity? entity = await passengerReadRepository
            .GetByIdAsync(id, cancellationToken);

        if (entity is not null)
        {
            return Result<PassengerEntity>.WithSuccess(entity);
        }

        Error error = DomainErrors.PassengerError.NotFound;

        logger.LogNotFound(
            nameof(GetByIdQueryHandler),
            nameof(GetByIdAsync), id, error);

        return Result<PassengerEntity>.FromNotFound(error);
    }
}
