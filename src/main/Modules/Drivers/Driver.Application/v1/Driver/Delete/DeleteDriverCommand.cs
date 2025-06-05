using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Driver.Application.v1.Driver.Delete;

public sealed record DeleteDriverCommand(Guid Id) : IRequest<Result>;
