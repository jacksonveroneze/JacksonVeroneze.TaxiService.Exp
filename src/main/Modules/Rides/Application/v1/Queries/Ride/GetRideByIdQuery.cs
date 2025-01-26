using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Rides.Application.v1.Models.Ride;

namespace TaxiService.Modules.Rides.Application.v1.Queries.Ride;

public sealed record GetRideByIdQuery(Guid Id) :
    IRequest<Result<GetRideByIdQueryResponse>>;