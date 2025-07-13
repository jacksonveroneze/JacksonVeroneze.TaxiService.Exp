using TaxiService.Common.BaseApplication.Validators;

namespace TaxiService.Modules.Ride.Application.v1.Ride.Accept;

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
