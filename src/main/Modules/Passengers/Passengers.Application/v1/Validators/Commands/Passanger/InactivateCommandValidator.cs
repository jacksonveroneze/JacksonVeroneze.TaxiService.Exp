using TaxiService.Common.BaseApplication.Validators.Base;
using TaxiService.Modules.Passengers.Application.v1.Commands.Passanger;

namespace TaxiService.Modules.Passengers.Application.v1.Validators.Commands.Passanger;

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