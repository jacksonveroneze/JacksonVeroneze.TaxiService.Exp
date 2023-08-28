using JacksonVeroneze.TemplateWebApi.Application.Core.Errors;
using JacksonVeroneze.TemplateWebApi.Application.Core.Extensions;

namespace JacksonVeroneze.TemplateWebApi.Application.Validators.Base;

public class GuidValidator : AbstractValidator<Guid>
{
    public GuidValidator()
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
