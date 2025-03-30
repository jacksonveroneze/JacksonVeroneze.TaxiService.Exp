using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Drivers.Application.v1.Driver.Delete;

public sealed record DeleteCommand(Guid Id) : IRequest<Result>;