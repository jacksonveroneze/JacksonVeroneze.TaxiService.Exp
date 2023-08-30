using Bogus;
using JacksonVeroneze.TemplateWebApi.Application.Commands.User;

namespace JacksonVeroneze.TemplateWebApi.Util.Tests.Builders.Commands;

public static class ActivateUserCommandBuilder
{
    public static ActivateUserCommand BuildSingle()
    {
        return Factory().Generate();
    }

    public static ActivateUserCommand BuildSingleInvalid()
    {
        return Factory().Generate();
    }

    private static Faker<ActivateUserCommand> Factory(
        bool valid = true)
    {
        return new Faker<ActivateUserCommand>("pt_BR")
            .RuleFor(f => f.Id, s => valid ? s.Random.Guid() : Guid.Empty);
    }
}
