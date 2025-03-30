using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Drivers.Application.v1.Driver.Activate;

public sealed record ActivateCommand(Guid Id) : IRequest<Result>;