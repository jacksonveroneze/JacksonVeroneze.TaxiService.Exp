using TaxiService.Common.BaseApplication.Core.Errors;
using TaxiService.Common.BaseApplication.Core.Extensions;

namespace TaxiService.Common.BaseApplication.Validators.Base;

public class IdGuidValidator : AbstractValidator<Guid>
{
    public IdGuidValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithError(CommonValidationErrors.CommonErrors.IdIsRequired);
    }
}