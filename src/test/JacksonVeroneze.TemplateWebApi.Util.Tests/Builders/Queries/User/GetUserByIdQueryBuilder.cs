using Bogus;
using JacksonVeroneze.TemplateWebApi.Application.Queries.User;

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
            .CustomInstantiator(s => new GetUserByIdQuery(
                valid ? s.Random.Guid() : Guid.Empty));
    }
}
