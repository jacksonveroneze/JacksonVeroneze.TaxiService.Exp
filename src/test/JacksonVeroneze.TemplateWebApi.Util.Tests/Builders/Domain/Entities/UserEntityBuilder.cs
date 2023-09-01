using Bogus;
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

        if (totalIncludeEmail > 0)
        {
            entity.AddEmail(EmailEntityBuilder.BuildSingle());
        }

        return entity;
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
