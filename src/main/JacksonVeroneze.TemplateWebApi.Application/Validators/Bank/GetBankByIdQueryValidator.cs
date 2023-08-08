using JacksonVeroneze.TemplateWebApi.Application.Queries.Bank;

namespace JacksonVeroneze.TemplateWebApi.Application.Validators.Bank;

public class GetBankByIdQueryValidator : AbstractValidator<GetBankByIdQuery>
{
    public GetBankByIdQueryValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.Id)
            .SetValidator(new BankIdValidator());
    }
}
