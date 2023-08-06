using JacksonVeroneze.TemplateWebApi.Application.Queries.Old.State;

namespace JacksonVeroneze.TemplateWebApi.Application.Validators.Old.State;

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
