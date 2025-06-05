using TaxiService.Common.BaseApplication.Validators.Base;
using TaxiService.Modules.Driver.Domain.Enums;

namespace TaxiService.Modules.Driver.Application.v1.DriverAvailability.Update;

public class UpdateDriverAvailabilityCommandValidator
    : AbstractValidator<UpdateDriverAvailabilityCommand>
{
    public UpdateDriverAvailabilityCommandValidator()
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
