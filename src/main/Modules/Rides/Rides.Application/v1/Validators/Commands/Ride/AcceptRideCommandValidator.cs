using TaxiService.Common.BaseApplication.Validators.Base;
using TaxiService.Modules.Rides.Application.v1.Commands.Ride;

namespace TaxiService.Modules.Rides.Application.v1.Validators.Commands.Ride;

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
