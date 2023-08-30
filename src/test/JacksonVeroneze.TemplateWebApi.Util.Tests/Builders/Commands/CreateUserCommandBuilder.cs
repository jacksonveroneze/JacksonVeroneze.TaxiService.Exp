using Bogus;
using JacksonVeroneze.TemplateWebApi.Application.Commands.User;
using JacksonVeroneze.TemplateWebApi.Domain.Enums;

namespace JacksonVeroneze.TemplateWebApi.Util.Tests.Builders.Commands;

public static class CreateUserCommandBuilder
{
    public static CreateUserCommand BuildSingle()
    {
        return Factory().Generate();
    }

    public static CreateUserCommand BuildSingleInvalid(int? lenghName,
        bool nullBirthday, bool nullGender)
    {
        return FactoryInvalid(lenghName, nullBirthday, nullGender)
            .Generate();
    }

    private static Faker<CreateUserCommand> Factory()
    {
        Gender[] genders = { Gender.Male, Gender.Female };

        return new Faker<CreateUserCommand>("pt_BR")
            .RuleFor(f => f.Name, s => s.Person.FullName)
            .RuleFor(f => f.Birthday,
                s => DateOnly.FromDateTime(s.Date.Recent()))
            .RuleFor(f => f.Gender, s => s.PickRandom(genders));
    }

    private static Faker<CreateUserCommand> FactoryInvalid(
        int? lenghName,
        bool nullBirthday,
        bool nullGender)
    {
        DateOnly? dateOnly = !nullBirthday ? null : DateOnly.FromDateTime(DateTime.Now);

        Gender[] genders = { Gender.Male, Gender.Female };

        return new Faker<CreateUserCommand>("pt_BR")
            .RuleFor(f => f.Name, f => lenghName is null ? null : f.Random.String2(lenghName.Value))
            .RuleFor(f => f.Birthday, s => nullBirthday ? null : DateOnly.FromDateTime(s.Date.Past()))
            .RuleFor(f => f.Gender, s => nullGender ? null : s.PickRandom(genders));
    }
}
