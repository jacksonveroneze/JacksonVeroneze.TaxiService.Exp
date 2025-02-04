using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Passengers.Application.v1.Models.User;

namespace TaxiService.Modules.Passengers.Application.v1.Queries.User;

public sealed record GetUserByIdQuery(Guid Id) :
    IRequest<Result<GetUserByIdQueryResponse>>;