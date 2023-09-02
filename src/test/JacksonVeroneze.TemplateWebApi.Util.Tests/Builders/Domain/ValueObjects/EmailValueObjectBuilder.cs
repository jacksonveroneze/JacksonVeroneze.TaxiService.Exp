using Bogus;
using JacksonVeroneze.TemplateWebApi.Domain.ValueObjects;

namespace JacksonVeroneze.TemplateWebApi.Util.Tests.Builders.Domain.ValueObjects;

public static class EmailValueObjectBuilder
{
    public static EmailValueObject BuildSingle(
        string? value = null)
    {
        return Factory(value).Generate();
    }

    private static Faker<EmailValueObject> Factory(
        string? value = null)
    {
        return new Faker<EmailValueObject>("pt_BR")
            .CustomInstantiator(conf =>
                EmailValueObject.Create(value ?? conf.Person.Email).Value!);
    }
}
