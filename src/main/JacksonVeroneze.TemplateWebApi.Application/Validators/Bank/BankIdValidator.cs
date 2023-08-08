namespace JacksonVeroneze.TemplateWebApi.Application.Validators.Bank;

public class BankIdValidator : AbstractValidator<Guid>
{
    public BankIdValidator()
    {
        RuleFor(request => request)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty();
    }
}
