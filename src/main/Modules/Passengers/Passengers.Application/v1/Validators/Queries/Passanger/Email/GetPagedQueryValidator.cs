using TaxiService.Common.BaseApplication.Validators.Base;
using TaxiService.Modules.Passengers.Application.v1.Queries.Passanger.Email;

namespace TaxiService.Modules.Passengers.Application.v1.Validators.Queries.Passanger.Email;

public class GetPagedQueryValidator : AbstractValidator<GetPagedQuery>
{
    public GetPagedQueryValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.PassangerId)
            .SetValidator(new IdGuidValidator());
    }
}