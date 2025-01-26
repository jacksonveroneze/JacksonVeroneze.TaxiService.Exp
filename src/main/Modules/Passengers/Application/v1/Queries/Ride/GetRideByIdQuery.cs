using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Passengers.Application.v1.Models.Ride;

namespace TaxiService.Modules.Passengers.Application.v1.Queries.Ride;

public sealed record GetRideByIdQuery(Guid Id) :
    IRequest<Result<GetRideByIdQueryResponse>>;