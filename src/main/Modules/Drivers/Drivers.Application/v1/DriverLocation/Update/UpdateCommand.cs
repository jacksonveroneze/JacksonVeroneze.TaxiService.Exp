using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Drivers.Application.v1.DriverLocation.Update;

public sealed record UpdateCommand(
    Guid Id,
    float Latitude,
    float Longitude) : IRequest<Result>;
