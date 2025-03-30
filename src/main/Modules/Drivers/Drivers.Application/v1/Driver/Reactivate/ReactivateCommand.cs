using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Drivers.Application.v1.Driver.Reactivate;

public sealed record ReactivateCommand(Guid Id) : IRequest<Result>;