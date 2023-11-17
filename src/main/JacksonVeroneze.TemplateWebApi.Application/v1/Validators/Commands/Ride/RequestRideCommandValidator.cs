using JacksonVeroneze.TemplateWebApi.Application.v1.Commands.Ride;
using JacksonVeroneze.TemplateWebApi.Application.v1.Validators.Base;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Validators.Commands.Ride;

public class RequestRideCommandValidator : AbstractValidator<RequestRideCommand>
{
    public RequestRideCommandValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.UserId)
            .SetValidator(new IdGuidValidator());

        RuleFor(request => request.LatitudeFrom)
            .Cascade(CascadeMode.Stop)
            .NotNull();

        RuleFor(request => request.LongitudeFrom)
            .Cascade(CascadeMode.Stop)
            .NotNull();

        RuleFor(request => request.LatitudeTo)
            .Cascade(CascadeMode.Stop)
            .NotNull();

        RuleFor(request => request.LongitudeTo)
            .Cascade(CascadeMode.Stop)
            .NotNull();
    }
}
