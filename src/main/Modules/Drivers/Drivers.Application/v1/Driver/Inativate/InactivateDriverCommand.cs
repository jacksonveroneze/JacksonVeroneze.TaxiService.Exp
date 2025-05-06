using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Drivers.Application.v1.Driver.Inativate;

public sealed record InactivateDriverCommand(Guid Id) : IRequest<Result>;
