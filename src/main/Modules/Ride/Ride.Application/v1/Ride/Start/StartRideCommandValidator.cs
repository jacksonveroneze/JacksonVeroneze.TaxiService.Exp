using TaxiService.Common.BaseApplication.Validators;

namespace TaxiService.Modules.Ride.Application.v1.Ride.Start;

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
