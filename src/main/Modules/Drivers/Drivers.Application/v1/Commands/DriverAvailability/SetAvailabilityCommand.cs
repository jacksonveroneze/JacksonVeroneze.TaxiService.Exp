using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Drivers.Domain.Enums;

namespace TaxiService.Modules.Drivers.Application.v1.Commands.DriverAvailability;

public sealed record SetAvailabilityCommand(
    Guid Id,
    DriverAvailabilityStatus Status) : IRequest<Result>;