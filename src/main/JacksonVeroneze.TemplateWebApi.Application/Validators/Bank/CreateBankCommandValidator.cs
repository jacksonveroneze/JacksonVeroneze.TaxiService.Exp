using JacksonVeroneze.TemplateWebApi.Application.Commands.Bank;

namespace JacksonVeroneze.TemplateWebApi.Application.Validators.Bank;

public class CreateBankCommandValidator : AbstractValidator<CreateBankCommand>
{
    private const int MinLength = 3;
    private const int MaxLength = 100;

    public CreateBankCommandValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.Name)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .Length(MinLength, MaxLength);
    }
}
