using Bogus;
using JacksonVeroneze.TemplateWebApi.Application.v1.Commands.User.Email;

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
            .RuleFor(f => f.Id, s =>
                valid ? userId ?? s.Random.Guid() : Guid.Empty)
            .RuleFor(f => f.EmailId, s =>
                valid ? emailId ?? s.Random.Guid() : Guid.Empty);
    }
}
