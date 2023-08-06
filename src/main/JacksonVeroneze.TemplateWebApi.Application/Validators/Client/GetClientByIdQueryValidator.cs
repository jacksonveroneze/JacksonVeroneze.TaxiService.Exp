using JacksonVeroneze.TemplateWebApi.Application.Queries.Client;

namespace JacksonVeroneze.TemplateWebApi.Application.Validators.Client;

public class GetClientByIdQueryValidator : AbstractValidator<GetClientByIdQuery>
{
    public GetClientByIdQueryValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.Id)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty();
    }
}
