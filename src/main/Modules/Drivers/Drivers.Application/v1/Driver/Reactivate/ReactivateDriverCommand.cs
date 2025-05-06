using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Drivers.Application.v1.Driver.Reactivate;

public sealed record ReactivateDriverCommand(Guid Id) : IRequest<Result>;
