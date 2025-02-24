using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Drivers.Application.v1.Commands.DriverAvailability;

public sealed record UpdateLocationCommand(
    Guid Id,
    float Latitude,
    float Longitude) : IRequest<Result>;