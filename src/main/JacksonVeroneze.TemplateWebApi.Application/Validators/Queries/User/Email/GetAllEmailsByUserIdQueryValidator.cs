using JacksonVeroneze.TemplateWebApi.Application.Queries.User.Email;
using JacksonVeroneze.TemplateWebApi.Application.Validators.Base;

namespace JacksonVeroneze.TemplateWebApi.Application.Validators.Queries.User.Email;

public class GetAllEmailsByUserIdQueryValidator : AbstractValidator<GetAllEmailsByUserIdQuery>
{
    public GetAllEmailsByUserIdQueryValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.Id)
            .SetValidator(new IdGuidValidator());
    }
}
