using TaxiService.Common.BaseApplication.Validators.Base;
using TaxiService.Modules.Rides.Application.v1.Queries.Ride;
using TaxiService.Modules.Rides.Domain.Enums;

namespace TaxiService.Modules.Rides.Application.v1.Validators.Queries.Ride;

public class GetPagedQueryValidator : AbstractValidator<GetPagedQuery>
{
    public GetPagedQueryValidator()
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