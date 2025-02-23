using TaxiService.Common.BaseApplication.Validators.Base;
using TaxiService.Modules.Drivers.Application.v1.Commands.Driver;

namespace TaxiService.Modules.Drivers.Application.v1.Validators.Commands.Driver;

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