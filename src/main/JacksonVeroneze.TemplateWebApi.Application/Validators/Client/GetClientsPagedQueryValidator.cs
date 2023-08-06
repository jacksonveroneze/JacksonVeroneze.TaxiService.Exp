using JacksonVeroneze.TemplateWebApi.Application.Queries.Client;
using JacksonVeroneze.TemplateWebApi.Application.Validators.Base;

namespace JacksonVeroneze.TemplateWebApi.Application.Validators.Client;

public class GetClientsPagedQueryValidator : AbstractValidator<GetClientPagedQuery>
{
    public GetClientsPagedQueryValidator()
    {
        RuleFor(request => request)
            .NotNull()
            .SetValidator(new PagedRequestValidator());
    }
}
