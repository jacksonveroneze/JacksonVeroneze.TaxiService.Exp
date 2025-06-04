using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Common.BaseApplication.Interfaces.Mapper;
using TaxiService.Modules.Rides.Application.Common.Interfaces.Repositories;
using TaxiService.Modules.Rides.Application.Common.Interfaces.Services;
using TaxiService.Modules.Rides.Application.v1.Ride.Common.Models;
using TaxiService.Modules.Rides.Domain.Core.Errors;

namespace TaxiService.Modules.Rides.Application.v1.Ride.GetById;

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
            return Result<GetRideByIdQueryResponse>
                .FromNotFound(DomainErrors.RideError.NotFound);
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
