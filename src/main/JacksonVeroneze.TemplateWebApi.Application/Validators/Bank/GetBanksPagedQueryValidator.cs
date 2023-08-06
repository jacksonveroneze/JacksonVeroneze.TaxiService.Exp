using JacksonVeroneze.TemplateWebApi.Application.Queries.Bank;
using JacksonVeroneze.TemplateWebApi.Application.Validators.Base;

namespace JacksonVeroneze.TemplateWebApi.Application.Validators.Bank;

public class GetBanksPagedQueryValidator : AbstractValidator<GetBankPagedQuery>
{
    public GetBanksPagedQueryValidator()
    {
        RuleFor(request => request)
            .NotNull()
            .SetValidator(new PagedRequestValidator());
    }
}
