using TaxiService.Common.BaseApplication.Validators;

namespace TaxiService.Modules.Passenger.Application.v1.Email.Create;

public class CreateEmailCommandValidator
    : AbstractValidator<CreateEmailCommand>
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
