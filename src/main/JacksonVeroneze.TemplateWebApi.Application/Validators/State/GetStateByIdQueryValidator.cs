using JacksonVeroneze.TemplateWebApi.Application.Queries.State;

namespace JacksonVeroneze.TemplateWebApi.Application.Validators.State;

public class GetStateByIdQueryValidator : AbstractValidator<GetStateByIdQuery>
{
    public GetStateByIdQueryValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.Id)
            .Cascade(CascadeMode.Stop)
            .SetValidator(new StateQueryValidator()!);
    }
}
