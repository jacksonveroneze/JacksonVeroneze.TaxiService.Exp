using TaxiService.Common.BaseApplication.Validators.Base;

namespace TaxiService.Modules.Ride.Application.v1.Ride.Cancel;

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
