using TaxiService.Common.BaseApplication.Validators.Base;
using TaxiService.Modules.Passengers.Application.v1.Queries.Passenger;

namespace TaxiService.Modules.Passengers.Application.v1.Validators.Queries.Passenger;

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
