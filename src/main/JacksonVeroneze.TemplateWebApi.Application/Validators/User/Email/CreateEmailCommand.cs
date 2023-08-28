using JacksonVeroneze.TemplateWebApi.Application.Commands.User.Email;
using JacksonVeroneze.TemplateWebApi.Application.Core.Errors;
using JacksonVeroneze.TemplateWebApi.Application.Core.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Validators.Base;

namespace JacksonVeroneze.TemplateWebApi.Application.Validators.User.Email;

public class CreateEmailCommandValidator : AbstractValidator<CreateEmailCommand>
{
    public CreateEmailCommandValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.Id)
            .SetValidator(new GuidValidator());

        RuleFor(request => request.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithError(ValidationErrors.User.EmailIsRequired);
    }
}
