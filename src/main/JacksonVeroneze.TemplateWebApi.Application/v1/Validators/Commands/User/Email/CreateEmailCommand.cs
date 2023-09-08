using JacksonVeroneze.TemplateWebApi.Application.Core.Errors;
using JacksonVeroneze.TemplateWebApi.Application.Core.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.v1.Commands.User.Email;
using JacksonVeroneze.TemplateWebApi.Application.v1.Validators.Base;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Validators.Commands.User.Email;

public class CreateEmailCommandValidator : AbstractValidator<CreateEmailCommand>
{
    public CreateEmailCommandValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.Id)
            .SetValidator(new IdGuidValidator());

        RuleFor(request => request.Body)
            .NotNull();

        RuleFor(request => request.Body!.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithError(ValidationErrors.User.EmailIsRequired)
            .EmailAddress();
    }
}
