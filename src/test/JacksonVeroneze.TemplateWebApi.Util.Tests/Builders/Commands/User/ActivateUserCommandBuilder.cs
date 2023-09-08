using Bogus;
using JacksonVeroneze.TemplateWebApi.Application.v1.Commands.User;

namespace JacksonVeroneze.TemplateWebApi.Util.Tests.Builders.Commands.User;

public static class ActivateUserCommandBuilder
{
    public static ActivateUserCommand BuildSingle()
    {
        return Factory().Generate();
    }

    public static ActivateUserCommand BuildSingleInvalid()
    {
        return Factory(false).Generate();
    }

    private static Faker<ActivateUserCommand> Factory(
        bool valid = true)
    {
        return new Faker<ActivateUserCommand>("pt_BR")
            .CustomInstantiator(s => new ActivateUserCommand(
                valid ? s.Random.Guid() : Guid.Empty));
    }
}
