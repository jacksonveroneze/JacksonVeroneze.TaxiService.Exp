using Bogus;
using JacksonVeroneze.TemplateWebApi.Application.Queries.User;

namespace JacksonVeroneze.TemplateWebApi.Util.Tests.Builders.Queries.User;

public static class GetUserPagedQueryBuilder
{
    public static GetUserPagedQuery BuildSingle()
    {
        return Factory().Generate();
    }

    public static GetUserPagedQuery BuildSingleInvalid()
    {
        return Factory(false).Generate();
    }

    private static Faker<GetUserPagedQuery> Factory(
        bool valid = true)
    {
        return new Faker<GetUserPagedQuery>("pt_BR");
    }
}
