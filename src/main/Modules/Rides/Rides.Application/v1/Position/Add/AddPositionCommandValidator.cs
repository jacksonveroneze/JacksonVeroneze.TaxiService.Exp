using TaxiService.Common.BaseApplication.Validators.Base;

namespace TaxiService.Modules.Rides.Application.v1.Position.Add;

public class AddPositionCommandValidator : AbstractValidator<AddPositionCommand>
{
    public AddPositionCommandValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.RideId)
            .SetValidator(new IdGuidValidator());

        RuleFor(request => request.Latitude)
            .SetValidator(new CoordinateValidator());

        RuleFor(request => request.Longitude)
            .SetValidator(new CoordinateValidator());
    }
}
