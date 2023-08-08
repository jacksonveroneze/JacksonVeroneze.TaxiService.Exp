using JacksonVeroneze.TemplateWebApi.Application.Commands.Bank;

namespace JacksonVeroneze.TemplateWebApi.Application.Validators.Bank;

public class ActivateBankCommandValidator : AbstractValidator<ActivateBankCommand>
{
    public ActivateBankCommandValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.Id)
            .SetValidator(new BankIdValidator());
    }
}
