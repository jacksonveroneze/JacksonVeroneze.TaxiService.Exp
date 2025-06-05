using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Driver.Application.v1.Driver.Inactivate;

public sealed record InactivateDriverCommand(Guid Id) : IRequest<Result>;
