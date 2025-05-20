using TaxiService.Common.BaseApplication.Core.Extensions;
using TaxiService.Common.BaseApplication.Validators.Base;
using TaxiService.Modules.Passengers.Application.v1.Commands.Email;
using TaxiService.Modules.Passengers.Application.v1.Errors;

namespace TaxiService.Modules.Passengers.Application.v1.Validators.Commands.Passenger.Email;

public class CreateCommandValidator : AbstractValidator<CreateCommand>
{
    public CreateCommandValidator()
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
            .WithError(ValidationErrors.Passenger.EmailIsRequired)
            .EmailAddress();
    }
}
