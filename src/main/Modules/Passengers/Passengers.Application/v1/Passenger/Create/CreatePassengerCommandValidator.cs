using TaxiService.Common.BaseApplication.Interfaces.System;
using TaxiService.Common.BaseDomain.Validators;
using TaxiService.Modules.Passengers.Domain.Enums;

namespace TaxiService.Modules.Passengers.Application.v1.Passenger.Create;

public class CreatePassengerCommandValidator 
    : AbstractValidator<CreatePassengerCommand>
{
    private const int MinLengthName = 2;
    private const int MaxLengthName = 100;

    public CreatePassengerCommandValidator(IDateTimeProvider dateTime)
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.FullName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .Length(MinLengthName, MaxLengthName);

        RuleFor(request => request.Birthday)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .LessThan(dateTime.DateNow);

        RuleFor(request => request.Gender)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEqual(Gender.None)
            .IsInEnum();

        RuleFor(request => request.Document)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .Must(value => value!.All(char.IsNumber))
            .Must(CpfValidator.Validate);

        RuleFor(request => request.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .EmailAddress();
    }
}
