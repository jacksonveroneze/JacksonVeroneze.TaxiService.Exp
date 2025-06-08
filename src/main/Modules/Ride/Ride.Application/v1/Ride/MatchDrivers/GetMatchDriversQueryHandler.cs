using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Common.BaseApplication.Interfaces.Mapper;
using TaxiService.Modules.Ride.Application.Common.Interfaces.Repositories;
using TaxiService.Modules.Ride.Application.Common.Interfaces.Services;
using TaxiService.Modules.Ride.Application.Common.Interfaces.Services.Models;
using TaxiService.Modules.Ride.Application.v1.Ride.Common.Models;
using TaxiService.Modules.Ride.Domain;

namespace TaxiService.Modules.Ride.Application.v1.Ride.MatchDrivers;

public sealed class GetMatchDriversQueryHandler(
    ILogger<GetMatchDriversQueryHandler> logger,
    IDataMapper mapper,
    IRideReadRepository rideReadRepository,
    IRideMatchDriversService rideMatchDriversService)
    : IRequestHandler<GetMatchDriversQuery, Result<GetMatchDriversQueryResponse>>
{
    public async Task<Result<GetMatchDriversQueryResponse>> Handle(
        GetMatchDriversQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        RideDto? rideDto = await rideReadRepository
            .GetByIdAsync(request.RideId, cancellationToken);

        RideMatchFilter filterMatches = new()
        {
            Latitude = rideDto!.From.Latitude,
            Longitude = rideDto.From.Longitude,
            PassengerId = request.RideId,
        };

        ICollection<DriverMatchCandidate> matches =
            await rideMatchDriversService.GetMatchDriversAsync(
                filterMatches, cancellationToken);

        GetMatchDriversQueryResponse response =
            mapper.Map<GetMatchDriversQueryResponse>(matches);

        logger.LogGetAll(
            ModuleConstants.ContextName,
            nameof(GetMatchDriversQueryHandler),
            nameof(Handle), matches.Count);

        return Result<GetMatchDriversQueryResponse>
            .WithSuccess(response);
    }
}
