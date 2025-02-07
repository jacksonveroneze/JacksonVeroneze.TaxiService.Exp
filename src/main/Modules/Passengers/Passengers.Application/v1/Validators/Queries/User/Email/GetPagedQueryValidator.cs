using TaxiService.Common.BaseApplication.Validators.Base;
using TaxiService.Modules.Passengers.Application.v1.Queries.User.Email;

namespace TaxiService.Modules.Passengers.Application.v1.Validators.Queries.User.Email;

public class GetPagedQueryValidator : AbstractValidator<GetPagedQuery>
{
    public GetPagedQueryValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.UserId)
            .SetValidator(new IdGuidValidator());
    }
}