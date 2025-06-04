using JacksonVeroneze.NET.Result;

using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Common.BaseApplication.Interfaces.Mapper;
using TaxiService.Modules.Passengers.Application.Common.Interfaces.Repositories;
using TaxiService.Modules.Passengers.Application.v1.Passenger.Common.Models;
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

        PassengerDto? item = await passengerReadRepository
            .GetByIdAsync(request.Id, cancellationToken);

        if (item is null)
        {
            return Result<GetPassengerByIdQueryResponse>.FromNotFound(
                DomainErrors.PassengerError.NotFound);
        }

        GetPassengerByIdQueryResponse response =
            mapper.Map<GetPassengerByIdQueryResponse>(item);

        logger.LogGetById(nameof(GetPassengerByIdQueryHandler),
            nameof(Handle), request.Id);

        return Result<GetPassengerByIdQueryResponse>
            .WithSuccess(response);
    }
}
