using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Drivers.Application.v1.Driver.Inactivate;

public sealed record InactivateDriverCommand(Guid Id) : IRequest<Result>;
