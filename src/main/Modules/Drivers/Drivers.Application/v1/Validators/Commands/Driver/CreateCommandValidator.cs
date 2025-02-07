using TaxiService.Common.BaseApplication.Core.Extensions;
using TaxiService.Common.BaseApplication.Interfaces.System;
using TaxiService.Common.BaseDomain.Enums;
using TaxiService.Common.BaseDomain.Validators;
using TaxiService.Modules.Drivers.Application.v1.Commands.Driver;
using TaxiService.Modules.Drivers.Application.v1.Errors;

namespace TaxiService.Modules.Drivers.Application.v1.Validators.Commands.Driver;

public class CreateCommandValidator : AbstractValidator<CreateCommand>
{
    private const int MinLengthName = 2;
    private const int MaxLengthName = 100;

    public CreateCommandValidator(IDateTimeProvider dateTime)
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.FullName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithError(ValidationErrors.User.NameIsRequired)
            .Length(MinLengthName, MaxLengthName);

        RuleFor(request => request.Birthday)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .WithError(ValidationErrors.User.BirthdayIsRequired)
            .LessThan(dateTime.DateNow);

        RuleFor(request => request.Gender)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .WithError(ValidationErrors.User.GenderIsRequired)
            .NotEqual(Gender.None)
            .IsInEnum();

        RuleFor(request => request.Document)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithError(ValidationErrors.User.DocumentIsRequired)
            .Must(value => value!.All(char.IsNumber))
            .Must(CpfValidator.Validate)
            .WithError(ValidationErrors.User.DocumentIsInvalid);

        RuleFor(request => request.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithError(ValidationErrors.User.EmailIsRequired)
            .EmailAddress()
            .WithError(ValidationErrors.User.EmailIsInvalid);
    }
}