using JacksonVeroneze.TemplateWebApi.Application.Commands.User;

namespace JacksonVeroneze.TemplateWebApi.Application.Validators.User;

public class ActivateUserCommandValidator : AbstractValidator<ActivateUserCommand>
{
    public ActivateUserCommandValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.Id)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty();
    }
}
