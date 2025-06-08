using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Driver.Application.v1.DriverLocation.Update;

public sealed record UpdateDriverLocationCommand(
    Guid Id,
    float Latitude,
    float Longitude) : IRequest<Result>;
