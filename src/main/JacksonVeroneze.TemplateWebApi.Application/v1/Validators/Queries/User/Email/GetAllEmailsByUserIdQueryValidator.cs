using JacksonVeroneze.TemplateWebApi.Application.v1.Queries.User.Email;
using JacksonVeroneze.TemplateWebApi.Application.v1.Validators.Base;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Validators.Queries.User.Email;

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
