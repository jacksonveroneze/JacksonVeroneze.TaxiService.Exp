using Bogus;
using JacksonVeroneze.TemplateWebApi.Application.v1.Commands.User.Email;

namespace JacksonVeroneze.TemplateWebApi.Util.Tests.Builders.Commands.User.Email;

public static class CreateEmailCommandBuilder
{
    public static CreateEmailCommand BuildSingle()
    {
        return Factory().Generate();
    }

    public static CreateEmailCommand BuildSingleInvalid()
    {
        return Factory(false).Generate();
    }

    private static Faker<CreateEmailCommand> Factory(
        bool valid = true)
    {
        return new Faker<CreateEmailCommand>("pt_BR")
            .RuleFor(f => f.Id, s => valid ? s.Random.Guid() : Guid.Empty)
            .RuleFor(f => f.Body, FactoryBody(valid).Generate());
    }

    private static Faker<CreateEmailBodyCommand> FactoryBody(
        bool valid = true)
    {
        return new Faker<CreateEmailBodyCommand>("pt_BR")
            .RuleFor(f => f.Email, s => valid ? s.Person.Email : "");
    }
}