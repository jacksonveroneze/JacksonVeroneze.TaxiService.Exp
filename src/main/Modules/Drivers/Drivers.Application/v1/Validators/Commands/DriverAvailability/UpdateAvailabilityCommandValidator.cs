using TaxiService.Common.BaseApplication.Validators.Base;
using TaxiService.Modules.Drivers.Application.v1.Commands.DriverAvailability;
using TaxiService.Modules.Drivers.Domain.Enums;

namespace TaxiService.Modules.Drivers.Application.v1.Validators.Commands.DriverAvailability;

public class UpdateAvailabilityCommandValidator
    : AbstractValidator<UpdateAvailabilityCommand>
{
    public UpdateAvailabilityCommandValidator()
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

        RuleFor(request => request.Latitude)
            .SetValidator(new CoordinateValidator());

        RuleFor(request => request.Longitude)
            .SetValidator(new CoordinateValidator());
    }
}