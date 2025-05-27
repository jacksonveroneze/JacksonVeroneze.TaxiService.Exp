using TaxiService.Common.BaseApplication.Validators.Base;
using TaxiService.Modules.Passengers.Application.v1.Commands.Email;

namespace TaxiService.Modules.Passengers.Application.v1.Validators.Commands.Passenger.Email;

public class CreateEmailCommandValidator : AbstractValidator<CreatePassengerCommand>
{
    public CreateEmailCommandValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.PassengerId)
            .SetValidator(new IdGuidValidator());

        RuleFor(request => request.Body)
            .NotNull();

        RuleFor(request => request.Body!.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .EmailAddress();
    }
}
