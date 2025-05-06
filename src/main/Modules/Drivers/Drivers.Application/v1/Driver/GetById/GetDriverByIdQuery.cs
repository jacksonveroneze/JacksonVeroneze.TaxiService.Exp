using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Drivers.Application.v1.Driver.GetById;

public sealed record GetDriverByIdQuery(Guid Id) :
    IRequest<Result<GetDriverByIdQueryResponse>>;
