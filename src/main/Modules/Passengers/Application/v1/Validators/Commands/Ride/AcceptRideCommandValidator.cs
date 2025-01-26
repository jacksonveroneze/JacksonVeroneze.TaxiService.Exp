using TaxiService.Modules.Passengers.Application.v1.Commands.Ride;
using TaxiService.Modules.Passengers.Application.Validators.Base;

namespace TaxiService.Modules.Passengers.Application.v1.Validators.Commands.Ride;

public class AcceptRideCommandValidator : AbstractValidator<AcceptRideCommand>
{
    public AcceptRideCommandValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.Id)
            .SetValidator(new IdGuidValidator());

        RuleFor(request => request.Body)
            .NotNull();

        RuleFor(request => request.Body!.DriverId)
            .SetValidator(new IdGuidValidator());
    }
}