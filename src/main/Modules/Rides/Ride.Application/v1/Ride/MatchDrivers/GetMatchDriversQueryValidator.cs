using TaxiService.Common.BaseApplication.Validators.Base;

namespace TaxiService.Modules.Ride.Application.v1.Ride.MatchDrivers;

public class GetMatchDriversQueryValidator 
    : AbstractValidator<GetMatchDriversQuery>
{
    public GetMatchDriversQueryValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.DriverId)
            .SetValidator(new IdGuidValidator());
    }
}
