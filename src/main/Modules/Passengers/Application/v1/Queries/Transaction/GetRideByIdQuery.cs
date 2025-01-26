using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Passengers.Application.v1.Models.Transaction;

namespace TaxiService.Modules.Passengers.Application.v1.Queries.Transaction;

public sealed record GetByRideIdQuery(Guid Id) :
    IRequest<Result<GetByRideIdQueryResponse>>;