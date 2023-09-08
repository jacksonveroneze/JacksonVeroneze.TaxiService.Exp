using Bogus;
using JacksonVeroneze.TemplateWebApi.Application.v1.Commands.User;

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
            .RuleFor(f => f.Id, s =>
                valid ? s.Random.Guid() : Guid.Empty);
    }
}
