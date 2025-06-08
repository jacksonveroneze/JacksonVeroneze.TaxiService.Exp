using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Common.BaseApplication.Interfaces.Mapper;
using TaxiService.Modules.Ride.Application.Common.Interfaces.Repositories;
using TaxiService.Modules.Ride.Application.Common.Interfaces.Services;
using TaxiService.Modules.Ride.Application.v1.Ride.Common.Models;
using TaxiService.Modules.Ride.Domain;
using TaxiService.Modules.Ride.Domain.Core.Errors;

namespace TaxiService.Modules.Ride.Application.v1.Ride.GetById;

public sealed class GetRideByIdQueryHandler(
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

        return Result<GetRideByIdQueryResponse>
            .WithSuccess(response);
    }
}
