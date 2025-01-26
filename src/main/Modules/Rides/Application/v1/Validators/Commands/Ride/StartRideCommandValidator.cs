using TaxiService.Modules.Rides.Application.v1.Commands.Ride;
using TaxiService.Common.Application.Validators.Base;

namespace TaxiService.Modules.Rides.Application.v1.Validators.Commands.Ride;

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