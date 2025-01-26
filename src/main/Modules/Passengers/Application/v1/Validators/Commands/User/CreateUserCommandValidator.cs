using TaxiService.Common.Application.Core.Extensions;
using TaxiService.Common.Application.Interfaces.System;
using TaxiService.Modules.Passengers.Application.v1.Commands.User;
using TaxiService.Modules.Passengers.Application.v1.Errors;
using TaxiService.Modules.Passengers.Domain.Enums;
using TaxiService.Modules.Passengers.Domain.Validators;

namespace TaxiService.Modules.Passengers.Application.v1.Validators.Commands.User;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    private const int MinLengthName = 2;
    private const int MaxLengthName = 100;

    public CreateUserCommandValidator(IDateTimeProvider dateTime)
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