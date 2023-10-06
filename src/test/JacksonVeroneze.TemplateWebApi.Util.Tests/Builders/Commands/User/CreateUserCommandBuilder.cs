using Bogus;
using Bogus.Extensions.Brazil;
using JacksonVeroneze.TemplateWebApi.Application.v1.Commands.User;
using JacksonVeroneze.TemplateWebApi.Domain.Enums;

namespace JacksonVeroneze.TemplateWebApi.Util.Tests.Builders.Commands.User;

public static class CreateUserCommandBuilder
{
    public static CreateUserCommand BuildSingle()
    {
        return Factory().Generate();
    }

    public static CreateUserCommand BuildInvalidSingle(
        bool invalidName = true, bool invalidBirthday = true,
        bool invalidGender = true, bool invalidCpf = true)
    {
        return FactoryInvalid(invalidName, invalidBirthday,
            invalidGender, invalidCpf).Generate();
    }

    private static Faker<CreateUserCommand> Factory()
    {
        Gender[] genders = { Gender.Male, Gender.Female };

        return new Faker<CreateUserCommand>("pt_BR")
            .RuleFor(f => f.Name, s => s.Person.FullName)
            .RuleFor(f => f.Birthday,
                s => DateOnly.FromDateTime(s.Date.Past()))
            .RuleFor(f => f.Gender, s => s.PickRandom(genders))
            .RuleFor(f => f.Document, s => s.Person.Cpf(false));
    }

    private static Faker<CreateUserCommand> FactoryInvalid(
        bool invalidName, bool invalidBirthday,
        bool invalidGender, bool invalidCpf)
    {
        Gender[] genders = { Gender.Male, Gender.Female };

        return new Faker<CreateUserCommand>("pt_BR")
            .RuleFor(f => f.Name, s =>
                invalidName ? "a" : s.Person.FullName)
            .RuleFor(f => f.Birthday, s =>
                invalidBirthday ? null : DateOnly.FromDateTime(s.Date.Past()))
            .RuleFor(f => f.Gender,
                s => invalidGender ? Gender.None : s.PickRandom(genders))
            .RuleFor(f => f.Document,
                s => invalidCpf ? string.Empty : s.Person.Cpf(false));
    }
}
