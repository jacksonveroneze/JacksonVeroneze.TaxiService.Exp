using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Drivers.Application.v1.Models.Driver;

namespace TaxiService.Modules.Drivers.Application.v1.Queries.Driver;

public sealed record GetByIdQuery(Guid Id) :
    IRequest<Result<GetByIdQueryResponse>>;