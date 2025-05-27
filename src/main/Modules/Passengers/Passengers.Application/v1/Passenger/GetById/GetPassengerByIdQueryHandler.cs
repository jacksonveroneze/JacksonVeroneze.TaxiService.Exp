using JacksonVeroneze.NET.Result;

using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Common.BaseApplication.Interfaces.Mapper;
using TaxiService.Modules.Passengers.Application.Common.Interfaces.Repositories.Passenger;
using TaxiService.Modules.Passengers.Domain.Core.Errors;

namespace TaxiService.Modules.Passengers.Application.v1.Passenger.GetById;

public sealed class GetPassengerByIdQueryHandler(
    ILogger<GetPassengerByIdQueryHandler> logger,
    IDataMapper mapper,
    IPassengerReadRepository passengerReadRepository)
    : IRequestHandler<GetPassengerByIdQuery, Result<GetPassengerByIdQueryResponse>>
{
    public async Task<Result<GetPassengerByIdQueryResponse>> Handle(
        GetPassengerByIdQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        Result<PassengerDto> resultEntity = await GetByIdAsync(
            request.Id, cancellationToken);

        if (resultEntity.IsFailure)
        {
            return Result<GetPassengerByIdQueryResponse>
                .FromNotFound(resultEntity.FirstError!);
        }

        GetPassengerByIdQueryResponse response =
            mapper.Map<GetPassengerByIdQueryResponse>(resultEntity.Value);

        logger.LogGetById(nameof(GetPassengerByIdQueryHandler),
            nameof(Handle), request.Id);

        return Result<GetPassengerByIdQueryResponse>
            .WithSuccess(response);
    }

    private async Task<Result<PassengerDto>> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        PassengerDto? entity = await passengerReadRepository
            .GetByIdAsync(id, cancellationToken);

        if (entity is not null)
        {
            return Result<PassengerDto>.WithSuccess(entity);
        }

        Error error = DomainErrors.PassengerError.NotFound;

        logger.LogNotFound(
            nameof(GetPassengerByIdQueryHandler),
            nameof(GetByIdAsync), id, error);

        return Result<PassengerDto>.FromNotFound(error);
    }
}
