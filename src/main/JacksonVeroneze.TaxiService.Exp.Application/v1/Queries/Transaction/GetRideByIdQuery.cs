using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Transaction;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Queries.Transaction;

public sealed record GetByRideIdQuery(Guid Id) :
    IRequest<Result<GetByRideIdQueryResponse>>;
