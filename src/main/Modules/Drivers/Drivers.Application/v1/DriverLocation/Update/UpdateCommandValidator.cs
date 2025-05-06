using TaxiService.Common.BaseApplication.Validators.Base;

namespace TaxiService.Modules.Drivers.Application.v1.DriverLocation.Update;

public class UpdateCommandValidator
    : AbstractValidator<UpdateCommand>
{
    public UpdateCommandValidator()
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
