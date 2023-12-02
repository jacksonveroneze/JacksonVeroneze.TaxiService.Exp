using JacksonVeroneze.TaxiService.Exp.Application.Core.Errors;
using JacksonVeroneze.TaxiService.Exp.Application.Core.Extensions;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Validators.Base;

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
