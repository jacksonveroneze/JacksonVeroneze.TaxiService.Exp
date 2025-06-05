using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Driver.Application.v1.Vehicle.GetById;

public sealed record GetVehicleByIdQuery(Guid Id) :
    IRequest<Result<GetVehicleByIdQueryResponse>>;
