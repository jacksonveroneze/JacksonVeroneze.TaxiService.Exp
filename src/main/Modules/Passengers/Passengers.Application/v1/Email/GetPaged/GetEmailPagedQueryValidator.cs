using TaxiService.Common.BaseApplication.Validators.Base;

namespace TaxiService.Modules.Passengers.Application.v1.Email.GetPaged;

public class GetEmailPagedQueryValidator
    : AbstractValidator<GetEmailPagedQuery>
{
    public GetEmailPagedQueryValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.PassengerId)
            .SetValidator(new IdGuidValidator());
    }
}
