using TaxiService.Common.BaseApplication.Validators.Base;

namespace TaxiService.Modules.Driver.Application.v1.DriverLocation.Update;

public class UpdateDriverLocationCommandValidator
    : AbstractValidator<UpdateDriverLocationCommand>
{
    public UpdateDriverLocationCommandValidator()
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
