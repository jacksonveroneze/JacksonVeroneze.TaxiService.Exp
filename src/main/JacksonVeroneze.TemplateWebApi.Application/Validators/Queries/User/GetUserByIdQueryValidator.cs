using JacksonVeroneze.TemplateWebApi.Application.Queries.User;
using JacksonVeroneze.TemplateWebApi.Application.Validators.Base;

namespace JacksonVeroneze.TemplateWebApi.Application.Validators.Queries.User;

public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
{
    public GetUserByIdQueryValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.Id)
            .SetValidator(new IdGuidValidator());
    }
}
