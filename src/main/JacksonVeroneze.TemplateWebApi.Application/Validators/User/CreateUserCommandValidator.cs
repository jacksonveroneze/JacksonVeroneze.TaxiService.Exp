using JacksonVeroneze.TemplateWebApi.Application.Commands.User;

namespace JacksonVeroneze.TemplateWebApi.Application.Validators.User;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.Name)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty();

        RuleFor(request => request.Birthday)
            .Cascade(CascadeMode.Stop)
            .NotNull();

        RuleFor(request => request.Gender)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .IsInEnum();
    }
}
