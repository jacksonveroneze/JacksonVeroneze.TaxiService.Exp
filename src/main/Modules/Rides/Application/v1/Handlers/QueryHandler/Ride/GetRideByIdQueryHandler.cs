using JacksonVeroneze.NET.Result;
using TaxiService.Common.Application.Extensions;
using TaxiService.Modules.Rides.Application.v1.Interfaces.Repositories.Ride;
using TaxiService.Modules.Rides.Application.v1.Models.Ride;
using TaxiService.Modules.Rides.Application.v1.Queries.Ride;
using TaxiService.Modules.Rides.Domain.Core.Errors;
using TaxiService.Modules.Rides.Domain.Entities;

namespace TaxiService.Modules.Rides.Application.v1.Handlers.QueryHandler.Ride;

public sealed class GetRideByIdQueryHandler(
    ILogger<GetRideByIdQueryHandler> logger,
    IMapper mapper,
    IRideReadRepository rideReadRepository)
    : IRequestHandler<GetRideByIdQuery, Result<GetRideByIdQueryResponse>>
{
    public async Task<Result<GetRideByIdQueryResponse>> Handle(
        GetRideByIdQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        RideEntity? entity = await rideReadRepository
            .GetByIdAsync(request.Id, cancellationToken);

        if (entity is null)
        {
            Error error = DomainErrors.RideError.NotFound;

            logger.LogNotFound(
                nameof(GetRideByIdQueryHandler),
                nameof(Handle), request.Id, error);

            return Result<GetRideByIdQueryResponse>
                .FromNotFound(error);
        }

        GetRideByIdQueryResponse response =
            mapper.Map<GetRideByIdQueryResponse>(entity);

        logger.LogGetById(nameof(GetRideByIdQueryHandler),
            nameof(Handle), request.Id);

        return Result<GetRideByIdQueryResponse>
            .WithSuccess(response);
    }
}