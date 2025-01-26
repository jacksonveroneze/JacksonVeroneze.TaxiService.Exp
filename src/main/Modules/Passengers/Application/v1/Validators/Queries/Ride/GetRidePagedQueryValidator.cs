using TaxiService.Modules.Passengers.Application.v1.Queries.Ride;
using TaxiService.Modules.Passengers.Application.Validators.Base;
using TaxiService.Modules.Passengers.Domain.Enums;

namespace TaxiService.Modules.Passengers.Application.v1.Validators.Queries.Ride;

public class GetRidePagedQueryValidator : AbstractValidator<GetRidePagedQuery>
{
    public GetRidePagedQueryValidator()
    {
        RuleFor(request => request)
            .SetValidator(new PagedCursorRequestValidator());

        When(request => request.Status != null, () =>
        {
            RuleFor(request => request.Status)
                .Cascade(CascadeMode.Stop)
                .IsInEnum()
                .NotEqual(RideStatus.None);
        });
    }
}