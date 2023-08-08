using JacksonVeroneze.TemplateWebApi.Application.Commands.Bank;

namespace JacksonVeroneze.TemplateWebApi.Application.Validators.Bank;

public class DeleteBankCommandValidator : AbstractValidator<DeleteBankCommand>
{
    public DeleteBankCommandValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.Id)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty();
    }
}
