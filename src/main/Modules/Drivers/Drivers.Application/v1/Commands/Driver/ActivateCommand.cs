using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Drivers.Application.v1.Commands.Driver;

public sealed record ActivateCommand(Guid Id) : IRequest<Result>;