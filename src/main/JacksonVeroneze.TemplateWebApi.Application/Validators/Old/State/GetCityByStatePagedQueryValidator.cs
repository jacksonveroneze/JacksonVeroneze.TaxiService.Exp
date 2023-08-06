using JacksonVeroneze.TemplateWebApi.Application.Queries.Old.City;

namespace JacksonVeroneze.TemplateWebApi.Application.Validators.Old.State;

public class GetCityByStatePagedQueryValidator :
    AbstractValidator<GetCityByStatePagedQuery>
{
    public GetCityByStatePagedQueryValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.StateId)
            .Cascade(CascadeMode.Stop)
            .SetValidator(new StateQueryValidator()!);
    }
}
