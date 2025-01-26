using TaxiService.Modules.Passengers.Application.v1.Commands.Ride;
using TaxiService.Modules.Passengers.Application.Validators.Base;

namespace TaxiService.Modules.Passengers.Application.v1.Validators.Commands.Ride;

public class CancelRideCommandValidator : AbstractValidator<CancelRideCommand>
{
    public CancelRideCommandValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.Id)
            .SetValidator(new IdGuidValidator());
    }
}