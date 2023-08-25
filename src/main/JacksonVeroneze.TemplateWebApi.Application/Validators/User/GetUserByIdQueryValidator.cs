using JacksonVeroneze.TemplateWebApi.Application.Queries.Client;

namespace JacksonVeroneze.TemplateWebApi.Application.Validators.User;

public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
{
    public GetUserByIdQueryValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.Id)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty();
    }
}
