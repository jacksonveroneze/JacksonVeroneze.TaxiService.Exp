using Bogus;
using JacksonVeroneze.TemplateWebApi.Application.v1.Queries.User;

namespace JacksonVeroneze.TemplateWebApi.Util.Tests.Builders.Queries.User;

public static class GetUserByIdQueryBuilder
{
    public static GetUserByIdQuery BuildSingle()
    {
        return Factory().Generate();
    }

    public static GetUserByIdQuery BuildSingleInvalid()
    {
        return Factory(false).Generate();
    }

    private static Faker<GetUserByIdQuery> Factory(
        bool valid = true)
    {
        return new Faker<GetUserByIdQuery>("pt_BR")
            .RuleFor(f => f.Id, s =>
                valid ? s.Random.Guid() : Guid.Empty);
    }
}
