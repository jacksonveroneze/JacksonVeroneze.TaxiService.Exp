using TaxiService.Common.BaseApplication.Validators.Base;
using TaxiService.Modules.Rides.Application.v1.Queries.Ride;

namespace TaxiService.Modules.Rides.Application.v1.Validators.Queries.Ride;

public class GetByIdQueryValidator : AbstractValidator<GetByIdQuery>
{
    public GetByIdQueryValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.Id)
            .SetValidator(new IdGuidValidator());
    }
}