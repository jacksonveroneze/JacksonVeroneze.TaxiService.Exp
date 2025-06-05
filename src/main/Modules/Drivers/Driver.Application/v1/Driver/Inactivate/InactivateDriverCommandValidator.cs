using TaxiService.Common.BaseApplication.Validators.Base;

namespace TaxiService.Modules.Driver.Application.v1.Driver.Inactivate;

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
