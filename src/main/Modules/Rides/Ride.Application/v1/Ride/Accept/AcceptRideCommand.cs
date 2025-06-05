using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Ride.Application.v1.Ride.Accept;

public sealed record AcceptRideCommand : IRequest<Result>
{
    public Guid Id { get; init; }

    public BodyCommand? Body { get; init; }

    public class BodyCommand
    {
        public Guid DriverId { get; init; }
    }
}
