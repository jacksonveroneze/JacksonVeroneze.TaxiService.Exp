using TaxiService.Common.BaseApplication.Validators.Base;
using TaxiService.Modules.Passengers.Application.v1.Commands.User;

namespace TaxiService.Modules.Passengers.Application.v1.Validators.Commands.User;

public class ActivateCommandValidator : AbstractValidator<ActivateCommand>
{
    public ActivateCommandValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.Id)
            .SetValidator(new IdGuidValidator());
    }
}