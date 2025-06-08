using TaxiService.Common.BaseApplication.Validators.Base;

namespace TaxiService.Modules.Passenger.Application.v1.Passenger.Activate;

public class ActivatePassengerCommandValidator
    : AbstractValidator<ActivatePassengerCommand>
{
    public ActivatePassengerCommandValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.Id)
            .SetValidator(new IdGuidValidator());
    }
}
