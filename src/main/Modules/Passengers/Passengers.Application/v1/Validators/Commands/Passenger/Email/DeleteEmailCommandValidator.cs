using TaxiService.Common.BaseApplication.Validators.Base;
using TaxiService.Modules.Passengers.Application.v1.Commands.Email;

namespace TaxiService.Modules.Passengers.Application.v1.Validators.Commands.Passenger.Email;

public class DeleteEmailCommandValidator : AbstractValidator<DeletePassengerCommand>
{
    public DeleteEmailCommandValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.Id)
            .SetValidator(new IdGuidValidator());

        RuleFor(request => request.EmailId)
            .SetValidator(new IdGuidValidator());
    }
}
