using Bogus;
using JacksonVeroneze.TemplateWebApi.Application.Commands.User;

namespace JacksonVeroneze.TemplateWebApi.Util.Tests.Builders.Commands.User;

public static class InactivateUserCommandBuilder
{
    public static InactivateUserCommand BuildSingle()
    {
        return Factory().Generate();
    }

    public static InactivateUserCommand BuildSingleInvalid()
    {
        return Factory(false).Generate();
    }

    private static Faker<InactivateUserCommand> Factory(
        bool valid = true)
    {
        return new Faker<InactivateUserCommand>("pt_BR")
            .CustomInstantiator(s => new InactivateUserCommand(
                valid ? s.Random.Guid() : Guid.Empty));
    }
}
