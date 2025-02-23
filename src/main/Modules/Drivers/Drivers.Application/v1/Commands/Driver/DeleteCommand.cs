using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Drivers.Application.v1.Commands.Driver;

public sealed record DeleteCommand(Guid Id) : IRequest<Result>;