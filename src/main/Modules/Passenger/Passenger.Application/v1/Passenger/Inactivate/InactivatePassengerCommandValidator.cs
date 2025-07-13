using TaxiService.Common.BaseApplication.Validators;

namespace TaxiService.Modules.Passenger.Application.v1.Passenger.Inactivate;

public class InactivatePassengerCommandValidator
    : AbstractValidator<InactivatePassengerCommand>
{
    public InactivatePassengerCommandValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.Id)
            .SetValidator(new IdGuidValidator());
    }
}
