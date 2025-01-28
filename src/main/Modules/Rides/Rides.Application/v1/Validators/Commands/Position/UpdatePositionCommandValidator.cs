using TaxiService.Common.BaseApplication.Validators.Base;
using TaxiService.Modules.Rides.Application.v1.Commands.Position;

namespace TaxiService.Modules.Rides.Application.v1.Validators.Commands.Position;

public class UpdatePositionCommandValidator : AbstractValidator<AddPositionCommand>
{
    public UpdatePositionCommandValidator()
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