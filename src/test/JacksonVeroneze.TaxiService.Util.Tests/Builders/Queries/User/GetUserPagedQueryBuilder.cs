using Bogus;
using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TemplateWebApi.Application.v1.Queries.User;

namespace JacksonVeroneze.TemplateWebApi.Util.Tests.Builders.Queries.User;

public static class GetUserPagedQueryBuilder
{
    public static GetUserPagedQuery BuildSingle()
    {
        return Factory().Generate();
    }

    private static Faker<GetUserPagedQuery> Factory()
    {
        return new Faker<GetUserPagedQuery>("pt_BR")
            .RuleFor(f => f.Page, s => s.Random.Int(1, 10))
            .RuleFor(f => f.PageSize, s => s.Random.Int(1, 10))
            .RuleFor(f => f.OrderBy, nameof(GetUserPagedQuery.Name))
            .RuleFor(f => f.Order, SortDirection.Ascending);
    }
}
