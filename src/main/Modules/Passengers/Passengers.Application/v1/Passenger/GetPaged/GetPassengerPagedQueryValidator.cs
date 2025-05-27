using TaxiService.Common.BaseApplication.Validators.Base;
using TaxiService.Modules.Passengers.Domain.Enums;

namespace TaxiService.Modules.Passengers.Application.v1.Passenger.GetPaged;

public class GetPassengerPagedQueryValidator : AbstractValidator<GetPassengerPagedQuery>
{
    private const int MinLengthName = 2;

    public GetPassengerPagedQueryValidator()
    {
        RuleFor(request => request)
            .SetValidator(new PagedCursorRequestValidator());

        When(request => !string.IsNullOrEmpty(request.Name), () =>
        {
            RuleFor(request => request.Name)
                .Cascade(CascadeMode.Stop)
                .MinimumLength(MinLengthName);
        });

        When(request => request.Status != null, () =>
        {
            RuleFor(request => request.Status)
                .Cascade(CascadeMode.Stop)
                .IsInEnum()
                .NotEqual(PassengerStatus.None);
        });
    }
}
