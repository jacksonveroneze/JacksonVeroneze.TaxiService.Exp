using JacksonVeroneze.TemplateWebApi.Application.Commands.Bank;

namespace JacksonVeroneze.TemplateWebApi.Application.Validators.Bank;

public class InactivateBankCommandValidator : AbstractValidator<InactivateBankCommand>
{
    public InactivateBankCommandValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.Id)
            .SetValidator(new BankIdValidator());
    }
}
