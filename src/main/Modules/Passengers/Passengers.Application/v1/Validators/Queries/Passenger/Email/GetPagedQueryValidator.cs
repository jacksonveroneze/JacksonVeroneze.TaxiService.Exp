using TaxiService.Common.BaseApplication.Validators.Base;
using TaxiService.Modules.Passengers.Application.v1.Queries.Passenger.Email;

namespace TaxiService.Modules.Passengers.Application.v1.Validators.Queries.Passenger.Email;

public class GetPagedQueryValidator : AbstractValidator<GetPagedQuery>
{
    public GetPagedQueryValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.PassengerId)
            .SetValidator(new IdGuidValidator());
    }
}
