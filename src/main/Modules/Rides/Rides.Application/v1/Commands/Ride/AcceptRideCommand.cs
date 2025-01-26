using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Rides.Application.v1.Commands.Ride;

public sealed record AcceptRideCommand : IRequest<Result>
{
    public Guid Id { get; init; }

    public BodyCommand? Body { get; init; }

    public class BodyCommand
    {
        public Guid DriverId { get; init; }
    }
}