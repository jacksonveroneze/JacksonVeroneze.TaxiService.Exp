using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Drivers.Application.v1.DriverLocation;

public sealed record SetLocationCommand(
    Guid Id,
    float Latitude,
    float Longitude) : IRequest<Result>;