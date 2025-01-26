using TaxiService.Common.Application.Validators.Base;
using TaxiService.Modules.Passengers.Application.v1.Queries.User.Email;

namespace TaxiService.Modules.Passengers.Application.v1.Validators.Queries.User.Email;

public class GetAllEmailsByUserIdQueryValidator : AbstractValidator<GetEmailPagedQuery>
{
    public GetAllEmailsByUserIdQueryValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.UserId)
            .SetValidator(new IdGuidValidator());
    }
}