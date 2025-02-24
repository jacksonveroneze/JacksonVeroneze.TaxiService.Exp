using TaxiService.Common.BaseApplication.Validators.Base;
using TaxiService.Modules.Drivers.Application.v1.Commands.Vehicle;

namespace TaxiService.Modules.Drivers.Application.v1.Validators.Commands.Vehicle;

public class CreateCommandValidator : AbstractValidator<CreateCommand>
{
    private const int MinLengthName = 2;
    private const int MaxLengthName = 100;

    public CreateCommandValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.DriverId)
            .SetValidator(new IdGuidValidator());

        RuleFor(request => request.Model)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .Length(MinLengthName, MaxLengthName);

        RuleFor(request => request.Brand)
            .Cascade(CascadeMode.Stop)
            .NotEmpty();

        RuleFor(request => request.Plate)
            .Cascade(CascadeMode.Stop)
            .NotEmpty();

        RuleFor(request => request.Color)
            .Cascade(CascadeMode.Stop)
            .NotEmpty();

        RuleFor(request => request.Year)
            .Cascade(CascadeMode.Stop)
            .NotNull();
    }
}