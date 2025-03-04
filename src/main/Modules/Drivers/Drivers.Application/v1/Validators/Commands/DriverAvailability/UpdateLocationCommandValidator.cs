using TaxiService.Common.BaseApplication.Validators.Base;
using TaxiService.Modules.Drivers.Application.v1.Commands.DriverAvailability;

namespace TaxiService.Modules.Drivers.Application.v1.Validators.Commands.DriverAvailability;

public class UpdateLocationCommandValidator
    : AbstractValidator<SetLocationCommand>
{
    public UpdateLocationCommandValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.Id)
            .SetValidator(new IdGuidValidator());

        RuleFor(request => request.Latitude)
            .SetValidator(new CoordinateValidator());

        RuleFor(request => request.Longitude)
            .SetValidator(new CoordinateValidator());
    }
}