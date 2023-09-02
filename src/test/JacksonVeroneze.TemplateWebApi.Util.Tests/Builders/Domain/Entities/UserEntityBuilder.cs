using Bogus;
using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.NET.Pagination.Extensions;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Enums;
using JacksonVeroneze.TemplateWebApi.Util.Tests.Builders.Domain.ValueObjects;

namespace JacksonVeroneze.TemplateWebApi.Util.Tests.Builders.Domain.Entities;

public static class UserEntityBuilder
{
    public static UserEntity BuildSingle(
        int totalIncludeEmail = 0)
    {
        UserEntity entity = Factory().Generate();

        for (int i = 1; i <= totalIncludeEmail; i++)
        {
            entity.AddEmail(EmailEntityBuilder.BuildSingle());
        }

        return entity;
    }

    public static Page<UserEntity> BuildPagedSingle(
        int total = 10)
    {
        List<UserEntity>? data = Factory()
            .Generate(total);

        PaginationParameters parameters = new(1, data.Count);

        return data.ToPageInMemory(parameters);
    }

    private static Faker<UserEntity> Factory()
    {
        return new Faker<UserEntity>("pt_BR")
            .CustomInstantiator(conf => new UserEntity(
                NameValueObjectBuilder.BuildSingle(),
                DateOnly.FromDateTime(conf.Date.Past()),
                Gender.Male,
                CpfValueObjectBuilder.BuildSingle()));
    }
}
