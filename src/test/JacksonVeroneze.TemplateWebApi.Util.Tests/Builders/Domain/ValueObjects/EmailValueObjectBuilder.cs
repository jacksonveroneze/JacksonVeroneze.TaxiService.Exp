using Bogus;
using JacksonVeroneze.TemplateWebApi.Domain.ValueObjects;

namespace JacksonVeroneze.TemplateWebApi.Util.Tests.Builders.Domain.ValueObjects;

public static class EmailValueObjectBuilder
{
    public static EmailValueObject BuildSingle()
    {
        return Factory().Generate();
    }

    private static Faker<EmailValueObject> Factory()
    {
        return new Faker<EmailValueObject>("pt_BR")
            .CustomInstantiator(conf =>
                EmailValueObject.Create(conf.Person.Email).Value!);
    }
}
