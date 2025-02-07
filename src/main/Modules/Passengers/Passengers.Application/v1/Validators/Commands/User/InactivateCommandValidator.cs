using TaxiService.Common.BaseApplication.Validators.Base;
using TaxiService.Modules.Passengers.Application.v1.Commands.User;

namespace TaxiService.Modules.Passengers.Application.v1.Validators.Commands.User;

public class InactivateCommandValidator : AbstractValidator<InactivateCommand>
{
    public InactivateCommandValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.Id)
            .SetValidator(new IdGuidValidator());
    }
}