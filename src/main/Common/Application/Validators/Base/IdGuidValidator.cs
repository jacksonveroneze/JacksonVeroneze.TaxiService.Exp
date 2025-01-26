using TaxiService.Common.Application.Core.Errors;
using TaxiService.Common.Application.Core.Extensions;

namespace TaxiService.Common.Application.Validators.Base;

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
            .WithError(CommonValidationErrors.Common.IdIsRequired);
    }
}