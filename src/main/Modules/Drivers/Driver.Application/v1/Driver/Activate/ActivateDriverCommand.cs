using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Driver.Application.v1.Driver.Activate;

public sealed record ActivateDriverCommand(Guid Id) : IRequest<Result>;
