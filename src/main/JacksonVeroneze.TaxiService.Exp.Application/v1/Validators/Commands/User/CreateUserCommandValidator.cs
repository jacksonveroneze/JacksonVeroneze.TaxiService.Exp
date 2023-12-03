using JacksonVeroneze.TaxiService.Exp.Application.Core.Errors;
using JacksonVeroneze.TaxiService.Exp.Application.Core.Extensions;
using JacksonVeroneze.TaxiService.Exp.Application.Interfaces.System;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Commands.User;
using JacksonVeroneze.TaxiService.Exp.Domain.Enums;
using JacksonVeroneze.TaxiService.Exp.Domain.Validators;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Validators.Commands.User;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    private const int MinLengthName = 2;
    private const int MaxLengthName = 100;

    public CreateUserCommandValidator(IDateTime dateTime)
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.Name)
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
            .NotEqual(GenderType.None)
            .IsInEnum();

        RuleFor(request => request.Document)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithError(ValidationErrors.User.DocumentIsRequired)
            .Must(value => value!.All(char.IsNumber))
            .Must(CpfValidator.Validate)
            .WithError(ValidationErrors.User.DocumentIsInvalid);
    }
}
