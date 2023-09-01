using Bogus;
using Bogus.Extensions.Brazil;
using JacksonVeroneze.TemplateWebApi.Application.Commands.User;
using JacksonVeroneze.TemplateWebApi.Domain.Enums;

namespace JacksonVeroneze.TemplateWebApi.Util.Tests.Builders.Commands;

public static class CreateUserCommandBuilder
{
    public static CreateUserCommand BuildSingle()
    {
        return Factory().Generate();
    }

    private static Faker<CreateUserCommand> Factory()
    {
        Gender[] genders = { Gender.Male, Gender.Female };

        return new Faker<CreateUserCommand>("pt_BR")
            .RuleFor(f => f.Name, s => s.Person.FullName)
            .RuleFor(f => f.Birthday,
                s => DateOnly.FromDateTime(s.Date.Past()))
            .RuleFor(f => f.Gender, s => s.PickRandom(genders))
            .RuleFor(f => f.Document, s => s.Person.Cpf());
    }
}
