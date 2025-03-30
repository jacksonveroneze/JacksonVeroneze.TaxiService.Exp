using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Drivers.Application.v1.Vehicle.GetById.Queries.Vehicle;

public sealed record GetByIdQuery(Guid Id) :
    IRequest<Result<GetByIdQueryResponse>>;