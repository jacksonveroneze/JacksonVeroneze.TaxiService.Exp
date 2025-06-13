using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Driver.Application.v1.Driver.Common.Enums;

namespace TaxiService.Modules.Driver.Application.v1.Driver.GetById;

public sealed record GetDriverByIdQuery(Guid Id) :
    IRequest<Result<GetDriverByIdQueryResponse>>
{
    public DriverExpands[]? Expand { get; init; }
}
