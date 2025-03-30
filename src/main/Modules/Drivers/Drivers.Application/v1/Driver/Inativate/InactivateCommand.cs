using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Drivers.Application.v1.Driver.Inativate;

public sealed record InactivateCommand(Guid Id) : IRequest<Result>;