using TaxiService.Common.BaseApplication.Validators.Base;
using TaxiService.Modules.Passengers.Application.v1.Commands.Passenger;

namespace TaxiService.Modules.Passengers.Application.v1.Validators.Commands.Passenger;

public class InactivatePassengerCommandValidator : AbstractValidator<InactivatePassengerCommand>
{
    public InactivatePassengerCommandValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.Id)
            .SetValidator(new IdGuidValidator());
    }
}
