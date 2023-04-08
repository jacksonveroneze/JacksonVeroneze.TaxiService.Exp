using JacksonVeroneze.TemplateWebApi.Application.Queries;

namespace JacksonVeroneze.TemplateWebApi.Application.Validators.State;

public class GetStateByIdQueryValidator : AbstractValidator<GetStateByIdQuery>
{
    public GetStateByIdQueryValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.Id)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .Length(2, 2)
            .Matches(@"[a-zA-Z]{2,2}");
    }
}