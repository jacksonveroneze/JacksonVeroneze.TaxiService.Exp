using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Driver.Domain.Enums;

namespace TaxiService.Modules.Driver.Application.v1.DriverAvailability.Update;

public sealed record UpdateDriverAvailabilityCommand(
    Guid Id,
    DriverAvailabilityStatus Status) : IRequest<Result>;
