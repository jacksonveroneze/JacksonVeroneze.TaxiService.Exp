using TaxiService.Common.BaseApplication.Validators.Base;

namespace TaxiService.Modules.Driver.Application.v1.Vehicle.Create;

public class CreateVehicleCommandValidator
    : AbstractValidator<CreateVehicleCommand>
{
    private const int MinLengthName = 2;
    private const int MaxLengthName = 100;

    public CreateVehicleCommandValidator()
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
