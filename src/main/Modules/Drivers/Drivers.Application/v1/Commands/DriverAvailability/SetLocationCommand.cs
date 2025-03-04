using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Drivers.Application.v1.Commands.DriverAvailability;

public sealed record SetLocationCommand(
    Guid Id,
    float Latitude,
    float Longitude) : IRequest<Result>;