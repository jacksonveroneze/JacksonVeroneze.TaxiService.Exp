using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Driver.Application.v1.Driver.GetById;

public sealed record GetDriverByIdQuery(Guid Id) :
    IRequest<Result<GetDriverByIdQueryResponse>>;
