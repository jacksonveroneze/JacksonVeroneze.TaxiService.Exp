using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Passengers.Application.v1.Models.Passenger;

namespace TaxiService.Modules.Passengers.Application.v1.Queries.Passenger;

public sealed record GetByIdQuery(Guid Id) :
    IRequest<Result<GetByIdQueryResponse>>;
