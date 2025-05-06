using TaxiService.Common.BaseApplication.Validators.Base;

namespace TaxiService.Modules.Drivers.Application.v1.Driver.Reactivate;

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
