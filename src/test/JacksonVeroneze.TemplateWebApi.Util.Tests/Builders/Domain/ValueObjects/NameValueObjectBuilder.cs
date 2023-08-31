using Bogus;
using JacksonVeroneze.TemplateWebApi.Domain.ValueObjects;

namespace JacksonVeroneze.TemplateWebApi.Util.Tests.Builders.Domain.ValueObjects;

public static class NameValueObjectBuilder
{
    public static NameValueObject BuildSingle()
    {
        return Factory().Generate();
    }

    private static Faker<NameValueObject> Factory()
    {
        return new Faker<NameValueObject>("pt_BR")
            .CustomInstantiator(conf =>
                NameValueObject.Create(conf.Person.FullName).Value!);
    }
}
