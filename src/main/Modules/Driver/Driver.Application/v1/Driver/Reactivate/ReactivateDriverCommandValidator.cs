using TaxiService.Common.BaseApplication.Validators;

namespace TaxiService.Modules.Driver.Application.v1.Driver.Reactivate;

public class ReactivateDriverCommandValidator
    : AbstractValidator<ReactivateDriverCommand>
{
    public ReactivateDriverCommandValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.Id)
            .SetValidator(new IdGuidValidator());
    }
}
