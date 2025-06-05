using TaxiService.Common.BaseApplication.Validators.Base;
using TaxiService.Modules.Ride.Domain.Enums;

namespace TaxiService.Modules.Ride.Application.v1.Ride.GetPaged;

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
