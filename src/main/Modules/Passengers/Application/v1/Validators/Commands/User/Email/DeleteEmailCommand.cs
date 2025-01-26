using TaxiService.Modules.Passengers.Application.v1.Commands.Email;
using TaxiService.Modules.Passengers.Application.Validators.Base;

namespace TaxiService.Modules.Passengers.Application.v1.Validators.Commands.User.Email;

public class DeleteEmailCommandValidator : AbstractValidator<DeleteEmailCommand>
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