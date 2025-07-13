using TaxiService.Common.BaseApplication.Models.Common;

namespace TaxiService.Modules.Ride.Application.v1.Ride.MatchDrivers;

public sealed record GetMatchDriversQueryResponse
    : CollectionResponse<MatchDriversResponse>;
