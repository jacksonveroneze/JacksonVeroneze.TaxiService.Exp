using TaxiService.Common.BaseApplication.Validators.Base;

namespace TaxiService.Modules.Drivers.Application.v1.Driver.Inativate;

public class InactivateDriverCommandValidator 
    : AbstractValidator<InactivateDriverCommand>
{
    public InactivateDriverCommandValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.Id)
            .SetValidator(new IdGuidValidator());
    }
}
