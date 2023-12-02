using JacksonVeroneze.TaxiService.Exp.Application.v1.Queries.Ride;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Validators.Base;
using JacksonVeroneze.TaxiService.Exp.Domain.Enums;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Validators.Queries.Ride;

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
                .IsInEnum()
                .NotEqual(RideStatus.None);
        });
    }
}
