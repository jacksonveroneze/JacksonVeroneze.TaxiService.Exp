using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Common.BaseApplication.Interfaces.Mapper;
using TaxiService.Modules.Rides.Application.v1.Interfaces.Repositories.Ride;
using TaxiService.Modules.Rides.Application.v1.Interfaces.Services;
using TaxiService.Modules.Rides.Application.v1.Models.Ride;
using TaxiService.Modules.Rides.Application.v1.Queries.Ride;
using TaxiService.Modules.Rides.Domain.Core.Errors;

namespace TaxiService.Modules.Rides.Application.v1.Handlers.QueryHandler.Ride;

public sealed class GetRideByIdQueryHandler(
    ILogger<GetRideByIdQueryHandler> logger,
    IDataMapper mapper,
    IRideReadRepository rideReadRepository,
    IRideExpandsService rideExpandsService)
    : IRequestHandler<GetRideByIdQuery, Result<GetRideByIdQueryResponse>>
{
    public async Task<Result<GetRideByIdQueryResponse>> Handle(
        GetRideByIdQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        RideDto? dto = await rideReadRepository
            .GetByIdAsync(request.Id, cancellationToken);

        if (dto is null)
        {
            Error error = DomainErrors.RideError.NotFound;

            logger.LogNotFound(
                nameof(GetRideByIdQueryHandler),
                nameof(Handle), request.Id, error);

            return Result<GetRideByIdQueryResponse>
                .FromNotFound(error);
        }

        GetRideByIdQueryResponse response =
            mapper.Map<GetRideByIdQueryResponse>(dto);

        await rideExpandsService.ApplyAsync(
            response.Data!, request.Expand, cancellationToken);
        
        logger.LogGetById(nameof(GetRideByIdQueryHandler),
            nameof(Handle), request.Id);

        return Result<GetRideByIdQueryResponse>
            .WithSuccess(response);
    }
}
