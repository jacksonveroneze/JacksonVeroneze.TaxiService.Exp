using JacksonVeroneze.TemplateWebApi.Application.Queries.Client;
using JacksonVeroneze.TemplateWebApi.Application.Validators.Base;

namespace JacksonVeroneze.TemplateWebApi.Application.Validators.User;

public class GetUserPagedQueryValidator : AbstractValidator<GetUserPagedQuery>
{
    public GetUserPagedQueryValidator()
    {
        RuleFor(request => request)
            .SetValidator(new PagedRequestValidator());
    }
}
