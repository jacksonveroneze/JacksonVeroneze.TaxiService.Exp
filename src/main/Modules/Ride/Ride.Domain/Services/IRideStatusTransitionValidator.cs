using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Ride.Domain.Enums;

namespace TaxiService.Modules.Ride.Domain.Services;

public interface IRideStatusTransitionValidator
{
    Result Validate(RideStatus current, RideStatus target);
}
