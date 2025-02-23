using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Drivers.Application.v1.Commands.Driver;

public sealed record InactivateCommand(Guid Id) : IRequest<Result>;