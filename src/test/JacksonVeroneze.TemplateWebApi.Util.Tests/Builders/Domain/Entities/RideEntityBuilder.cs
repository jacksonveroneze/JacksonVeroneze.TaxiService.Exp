using Bogus;
using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.NET.Pagination.Extensions;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Enums;
using JacksonVeroneze.TemplateWebApi.Domain.ValueObjects;
using JacksonVeroneze.TemplateWebApi.Util.Tests.Builders.Domain.ValueObjects;

namespace JacksonVeroneze.TemplateWebApi.Util.Tests.Builders.Domain.Entities;

public static class RideEntityBuilder
{
    public static RideEntity BuildSingle(
        int totalIncludePositions = 0)
    {
        RideEntity entity = Factory().Generate();

        for (int i = 1; i <= totalIncludePositions; i++)
        {
            entity.AddPosition(PositionEntityBuilder
                .BuildSingle(entity));
        }

        return entity;
    }

    private static Faker<RideEntity> Factory()
    {
        return new Faker<RideEntity>("pt_BR")
            .CustomInstantiator(_ => new RideEntity(
                UserEntityBuilder.BuildSingle(),
                CoordinateValueObjectBuilder.BuildSingle(),
                CoordinateValueObjectBuilder.BuildSingle()));
    }
}
