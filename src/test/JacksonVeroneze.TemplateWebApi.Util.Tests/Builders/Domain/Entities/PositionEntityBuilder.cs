using Bogus;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Util.Tests.Builders.Domain.ValueObjects;

namespace JacksonVeroneze.TemplateWebApi.Util.Tests.Builders.Domain.Entities;

public static class PositionEntityBuilder
{
    public static PositionEntity BuildSingle(
        RideEntity ride)
    {
        return Factory(ride).Generate();
    }

    private static Faker<PositionEntity> Factory(
        RideEntity ride)
    {
        return new Faker<PositionEntity>("pt_BR")
            .CustomInstantiator(_ => new PositionEntity(
                ride,
                CoordinateValueObjectBuilder.BuildSingle()));
    }
}
