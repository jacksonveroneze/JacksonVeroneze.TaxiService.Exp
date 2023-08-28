using JacksonVeroneze.TemplateWebApi.Application.Commands.User.Email;
using JacksonVeroneze.TemplateWebApi.Application.Validators.Base;

namespace JacksonVeroneze.TemplateWebApi.Application.Validators.User.Email;

public class DeleteEmailCommandValidator : AbstractValidator<DeleteEmailCommand>
{
    public DeleteEmailCommandValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.Id)
            .SetValidator(new GuidValidator());

        RuleFor(request => request.EmailId)
            .SetValidator(new GuidValidator());
    }
}
