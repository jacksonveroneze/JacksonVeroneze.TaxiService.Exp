using Bogus;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Util.Tests.Builders.Domain.ValueObjects;

namespace JacksonVeroneze.TemplateWebApi.Util.Tests.Builders.Domain.Entities;

public static class EmailEntityBuilder
{
    public static EmailEntity BuildSingle()
    {
        return Factory().Generate();
    }

    public static IEnumerable<EmailEntity> BuildMany(
        int total)
    {
        return Factory().Generate(total);
    }

    private static Faker<EmailEntity> Factory()
    {
        return new Faker<EmailEntity>("pt_BR")
            .CustomInstantiator(_ => new EmailEntity(
                EmailValueObjectBuilder.BuildSingle()));
    }
}
