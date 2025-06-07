using TaxiService.Common.BaseApplication.Models.Base;

namespace TaxiService.Modules.Ride.Application.v1.Ride.MatchDrivers;

public sealed record GetMatchDriversQueryResponse
    : CollectionResponse<MatchDriversResponse>;
