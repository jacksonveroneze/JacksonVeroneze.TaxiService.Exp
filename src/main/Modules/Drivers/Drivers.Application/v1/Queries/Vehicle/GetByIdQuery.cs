using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Drivers.Application.v1.Models.Vehicle;

namespace TaxiService.Modules.Drivers.Application.v1.Queries.Vehicle;

public sealed record GetByIdQuery(Guid Id) :
    IRequest<Result<GetByIdQueryResponse>>;