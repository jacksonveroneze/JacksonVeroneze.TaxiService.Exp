using JacksonVeroneze.TemplateWebApi.Application.Core.Errors;
using JacksonVeroneze.TemplateWebApi.Application.Core.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.System;
using JacksonVeroneze.TemplateWebApi.Application.v1.Commands.User;
using JacksonVeroneze.TemplateWebApi.Domain.Enums;
using JacksonVeroneze.TemplateWebApi.Domain.Util;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Validators.Commands.User;

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
            .NotEqual(Gender.None)
            .IsInEnum();

        RuleFor(request => request.Document)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithError(ValidationErrors.User.DocumentIsRequired)
            .Must(value => value!.All(char.IsNumber))
            .Must(ValidarCpf.Validar)
            .WithError(ValidationErrors.User.DocumentIsInvalid);
    }
}
