using TaxiService.Common.BaseApplication.Interfaces.System;
using TaxiService.Common.BaseDomain.Enums;
using TaxiService.Common.BaseDomain.Validators;

namespace TaxiService.Modules.Driver.Application.v1.Driver.Create;

public class CreateDriverCommandValidator
    : AbstractValidator<CreateDriverCommand>
{
    private const int MinLengthName = 2;
    private const int MaxLengthName = 100;

    private const int LengthCpf = 11;
    private const int MinAge = 18;

    public CreateDriverCommandValidator(IDateTimeProvider dateTime)
    {
        DateOnly minBirthday = dateTime.DateNow.AddYears(-MinAge);

        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.FullName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .Length(MinLengthName, MaxLengthName);

        RuleFor(request => request.Birthday)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .LessThan(minBirthday);

        RuleFor(request => request.Gender)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEqual(Gender.None)
            .IsInEnum();

        RuleFor(request => request.Phone)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty();

        RuleFor(request => request.Document)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .Length(LengthCpf)
            .Must(value => value!.All(char.IsNumber))
            .Must(CpfValidator.Validate);

        RuleFor(request => request.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .EmailAddress();
    }
}
