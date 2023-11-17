using JacksonVeroneze.TemplateWebApi.Application.v1.Commands.Ride;
using JacksonVeroneze.TemplateWebApi.Application.v1.Validators.Base;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Validators.Commands.Ride;

public class CancelRideCommandValidator : AbstractValidator<CancelRideCommand>
{
    public CancelRideCommandValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.Id)
            .SetValidator(new IdGuidValidator());
    }
}
