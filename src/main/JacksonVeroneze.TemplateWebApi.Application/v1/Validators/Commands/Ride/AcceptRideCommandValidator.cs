using JacksonVeroneze.TemplateWebApi.Application.v1.Commands.Ride;
using JacksonVeroneze.TemplateWebApi.Application.v1.Validators.Base;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Validators.Commands.Ride;

public class AcceptRideCommandValidator : AbstractValidator<AcceptRideCommand>
{
    public AcceptRideCommandValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.Id)
            .SetValidator(new IdGuidValidator());

        RuleFor(request => request.DriverId)
            .SetValidator(new IdGuidValidator());
    }
}
