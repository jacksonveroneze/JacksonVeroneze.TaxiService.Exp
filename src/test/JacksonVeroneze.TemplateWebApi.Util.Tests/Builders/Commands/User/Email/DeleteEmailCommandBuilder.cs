using Bogus;
using JacksonVeroneze.TemplateWebApi.Application.Commands.User.Email;

namespace JacksonVeroneze.TemplateWebApi.Util.Tests.Builders.Commands.User.Email;

public static class DeleteEmailCommandBuilder
{
    public static DeleteEmailCommand BuildSingle(
        Guid? userId = null, Guid? emailId = null)
    {
        return Factory(true, userId, emailId).Generate();
    }

    public static DeleteEmailCommand BuildSingleInvalid()
    {
        return Factory(false).Generate();
    }

    private static Faker<DeleteEmailCommand> Factory(
        bool valid = true,
        Guid? userId = null, Guid? emailId = null)
    {
        return new Faker<DeleteEmailCommand>("pt_BR")
            .CustomInstantiator(s => new DeleteEmailCommand(
                valid ? userId ?? s.Random.Guid() : Guid.Empty,
                valid ? emailId ?? s.Random.Guid() : Guid.Empty));
    }
}
