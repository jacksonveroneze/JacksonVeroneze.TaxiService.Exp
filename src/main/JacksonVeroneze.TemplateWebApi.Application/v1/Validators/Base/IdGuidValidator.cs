using JacksonVeroneze.TemplateWebApi.Application.Core.Errors;
using JacksonVeroneze.TemplateWebApi.Application.Core.Extensions;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Validators.Base;

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
            .WithError(ValidationErrors.User.IdIsRequired);
    }
}
