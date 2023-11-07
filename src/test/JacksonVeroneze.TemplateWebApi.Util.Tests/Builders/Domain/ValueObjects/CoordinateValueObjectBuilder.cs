using Bogus;
using JacksonVeroneze.TemplateWebApi.Domain.ValueObjects;

namespace JacksonVeroneze.TemplateWebApi.Util.Tests.Builders.Domain.ValueObjects;

public static class CoordinateValueObjectBuilder
{
    public static CoordinateValueObject BuildSingle()
    {
        return Factory().Generate();
    }

    private static Faker<CoordinateValueObject> Factory()
    {
        return new Faker<CoordinateValueObject>("pt_BR")
            .CustomInstantiator(f => CoordinateValueObject
                .Create(47.640320, 47.456411).Value!);
    }
}
