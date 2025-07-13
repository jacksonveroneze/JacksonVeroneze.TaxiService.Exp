using TaxiService.Common.BaseApplication.Validators;

namespace TaxiService.Modules.Ride.Application.v1.Ride.MatchDrivers;

public class GetMatchDriversQueryValidator 
    : AbstractValidator<GetMatchDriversQuery>
{
    public GetMatchDriversQueryValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.RideId)
            .SetValidator(new IdGuidValidator());
    }
}
