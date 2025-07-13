using TaxiService.Common.BaseApplication.Validators;

namespace TaxiService.Modules.Driver.Application.v1.Driver.Activate;

public class ActivateDriverCommandValidator
    : AbstractValidator<ActivateDriverCommand>
{
    public ActivateDriverCommandValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.Id)
            .SetValidator(new IdGuidValidator());
    }
}
