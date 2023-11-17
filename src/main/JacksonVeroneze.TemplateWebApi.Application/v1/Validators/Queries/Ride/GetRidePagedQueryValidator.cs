using JacksonVeroneze.TemplateWebApi.Application.v1.Queries.Ride;
using JacksonVeroneze.TemplateWebApi.Application.v1.Validators.Base;
using JacksonVeroneze.TemplateWebApi.Domain.Enums;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Validators.Queries.Ride;

public class GetRidePagedQueryValidator : AbstractValidator<GetRidePagedQuery>
{
    public GetRidePagedQueryValidator()
    {
        RuleFor(request => request)
            .SetValidator(new PagedRequestValidator());

        When(request => request.Status != null, () =>
        {
            RuleFor(request => request.Status)
                .Cascade(CascadeMode.Stop)
                .NotEqual(RideStatus.None)
                .IsInEnum();
        });
    }
}
