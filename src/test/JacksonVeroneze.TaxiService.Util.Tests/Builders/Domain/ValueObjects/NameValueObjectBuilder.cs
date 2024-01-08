using Bogus;
using JacksonVeroneze.TemplateWebApi.Domain.ValueObjects;

namespace JacksonVeroneze.TemplateWebApi.Util.Tests.Builders.Domain.ValueObjects;

public static class NameValueObjectBuilder
{
    public static NameValueObject BuildSingle(
        string? value = null)
    {
        return Factory(value).Generate();
    }

    private static Faker<NameValueObject> Factory(
        string? value = null)
    {
        return new Faker<NameValueObject>("pt_BR")
            .CustomInstantiator(conf =>
                NameValueObject.Create(value ?? conf.Person.FullName).Value!);
    }
}
