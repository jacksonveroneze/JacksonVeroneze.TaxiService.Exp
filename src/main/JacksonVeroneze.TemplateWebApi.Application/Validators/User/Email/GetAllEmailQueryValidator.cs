using JacksonVeroneze.TemplateWebApi.Application.Queries.User.Email;
using JacksonVeroneze.TemplateWebApi.Application.Validators.Base;

namespace JacksonVeroneze.TemplateWebApi.Application.Validators.User.Email;

public class GetAllEmailQueryValidator : AbstractValidator<GetAllEmailsByUserIdQuery>
{
    public GetAllEmailQueryValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.Id)
            .SetValidator(new GuidValidator());
    }
}
