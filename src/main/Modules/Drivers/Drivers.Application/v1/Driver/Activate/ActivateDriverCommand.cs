using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Drivers.Application.v1.Driver.Activate;

public sealed record ActivateDriverCommand(Guid Id) : IRequest<Result>;
