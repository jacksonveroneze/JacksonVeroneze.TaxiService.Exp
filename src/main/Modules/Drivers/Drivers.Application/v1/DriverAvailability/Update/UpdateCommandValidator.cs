using TaxiService.Common.BaseApplication.Validators.Base;
using TaxiService.Modules.Drivers.Domain.Enums;

namespace TaxiService.Modules.Drivers.Application.v1.DriverAvailability.Update;

public class UpdateCommandValidator
    : AbstractValidator<UpdateCommand>
{
    public UpdateCommandValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.Id)
            .SetValidator(new IdGuidValidator());

        RuleFor(request => request.Status)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEqual(DriverAvailabilityStatus.None)
            .IsInEnum();
    }
}
