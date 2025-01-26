using TaxiService.Common.Application.Core.Extensions;
using TaxiService.Common.Application.Validators.Base;
using TaxiService.Modules.Passengers.Application.v1.Commands.Email;
using TaxiService.Modules.Passengers.Application.v1.Errors;

namespace TaxiService.Modules.Passengers.Application.v1.Validators.Commands.User.Email;

public class CreateEmailCommandValidator : AbstractValidator<CreateEmailCommand>
{
    public CreateEmailCommandValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.UserId)
            .SetValidator(new IdGuidValidator());

        RuleFor(request => request.Body)
            .NotNull();

        RuleFor(request => request.Body!.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithError(ValidationErrors.User.EmailIsRequired)
            .EmailAddress();
    }
}