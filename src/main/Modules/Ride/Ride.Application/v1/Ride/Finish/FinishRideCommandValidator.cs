using TaxiService.Common.BaseApplication.Validators;

namespace TaxiService.Modules.Ride.Application.v1.Ride.Finish;

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
