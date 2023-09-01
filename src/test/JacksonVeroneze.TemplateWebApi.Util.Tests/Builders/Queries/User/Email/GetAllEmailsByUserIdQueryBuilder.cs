using Bogus;
using JacksonVeroneze.TemplateWebApi.Application.Queries.User.Email;

namespace JacksonVeroneze.TemplateWebApi.Util.Tests.Builders.Queries.User.Email;

public static class GetAllEmailsByUserIdQueryBuilder
{
    public static GetAllEmailsByUserIdQuery BuildSingle()
    {
        return Factory().Generate();
    }

    public static GetAllEmailsByUserIdQuery BuildSingleInvalid()
    {
        return Factory(false).Generate();
    }

    private static Faker<GetAllEmailsByUserIdQuery> Factory(
        bool valid = true)
    {
        return new Faker<GetAllEmailsByUserIdQuery>("pt_BR")
            .CustomInstantiator(s => new GetAllEmailsByUserIdQuery(
                valid ? s.Random.Guid() : Guid.Empty));
    }
}
