using TaxiService.Modules.Rides.Application.v1.Commands.Ride;
using TaxiService.Modules.Rides.Application.Validators.Base;

namespace TaxiService.Modules.Rides.Application.v1.Validators.Commands.Ride;

public class FinishRideCommandValidator : AbstractValidator<FinishRideCommand>
{
    public FinishRideCommandValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.Id)
            .SetValidator(new IdGuidValidator());
    }
}