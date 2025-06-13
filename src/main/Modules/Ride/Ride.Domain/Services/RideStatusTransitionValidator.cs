using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Ride.Domain.Enums;

namespace TaxiService.Modules.Ride.Domain.Services;

public sealed class RideStatusTransitionValidator : IRideStatusTransitionValidator
{
    private static readonly Dictionary<RideStatus, RideStatus[]> ValidTransitions = new()
    {
        [RideStatus.Requested] = [RideStatus.Accepted, RideStatus.Canceled],
        [RideStatus.Accepted] = [RideStatus.InProgress, RideStatus.Canceled],
        [RideStatus.InProgress] = [RideStatus.Completed, RideStatus.Canceled],
    };

    public Result Validate(RideStatus current, RideStatus target)
    {
        if (current == target)
        {
            return Result.FromInvalid(
                Error.Create("Ride.Status", "Ride is already in this status."));
        }

        if (!ValidTransitions.TryGetValue(current,
                out RideStatus[]? allowed) || !allowed.Contains(target))
        {
            return Result.FromRuleViolation(
                Error.Create("Ride.Status",
                    $"Cannot transition from {current} to {target}."));
        }

        return Result.WithSuccess();
    }
}
