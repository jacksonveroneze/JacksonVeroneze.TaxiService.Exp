using TaxiService.Modules.Passengers.Application.Core.Errors;
using TaxiService.Modules.Passengers.Application.Core.Extensions;

namespace TaxiService.Modules.Passengers.Application.Validators.Base;

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
            .WithError(ValidationErrors.Common.IdIsRequired);
    }
}