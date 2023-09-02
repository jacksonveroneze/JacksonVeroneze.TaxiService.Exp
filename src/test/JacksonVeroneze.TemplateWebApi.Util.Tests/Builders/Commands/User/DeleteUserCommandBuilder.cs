using Bogus;
using JacksonVeroneze.TemplateWebApi.Application.Commands.User;

namespace JacksonVeroneze.TemplateWebApi.Util.Tests.Builders.Commands.User;

public static class DeleteUserCommandBuilder
{
    public static DeleteUserCommand BuildSingle()
    {
        return Factory().Generate();
    }

    public static DeleteUserCommand BuildSingleInvalid()
    {
        return Factory(false).Generate();
    }

    private static Faker<DeleteUserCommand> Factory(
        bool valid = true)
    {
        return new Faker<DeleteUserCommand>("pt_BR")
            .CustomInstantiator(s => new DeleteUserCommand(
                valid ? s.Random.Guid() : Guid.Empty));
    }
}
