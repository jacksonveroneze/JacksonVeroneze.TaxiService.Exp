using JacksonVeroneze.TemplateWebApi.Application.Commands.User;
using JacksonVeroneze.TemplateWebApi.Application.Validators.Base;

namespace JacksonVeroneze.TemplateWebApi.Application.Validators.Commands.User;

public class InactivateUserCommandValidator : AbstractValidator<InactivateUserCommand>
{
    public InactivateUserCommandValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.Id)
            .SetValidator(new IdGuidValidator());
    }
}
