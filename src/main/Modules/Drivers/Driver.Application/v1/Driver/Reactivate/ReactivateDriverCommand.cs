using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Driver.Application.v1.Driver.Reactivate;

public sealed record ReactivateDriverCommand(Guid Id) : IRequest<Result>;
