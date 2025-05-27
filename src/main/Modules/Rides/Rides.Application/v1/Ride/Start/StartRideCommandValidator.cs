using TaxiService.Common.BaseApplication.Validators.Base;

namespace TaxiService.Modules.Rides.Application.v1.Ride.Start;

public class StartRideCommandValidator : AbstractValidator<StartRideCommand>
{
    public StartRideCommandValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.Id)
            .SetValidator(new IdGuidValidator());
    }
}
