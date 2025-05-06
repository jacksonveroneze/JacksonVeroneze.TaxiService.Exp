using JacksonVeroneze.NET.Result;

using TaxiService.Modules.Drivers.Domain.Enums;

namespace TaxiService.Modules.Drivers.Application.v1.DriverAvailability.Update;

public sealed record UpdateCommand(
    Guid Id,
    DriverAvailabilityStatus Status) : IRequest<Result>;
