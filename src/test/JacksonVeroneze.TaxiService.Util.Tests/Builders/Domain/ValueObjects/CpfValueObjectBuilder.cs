using Bogus;
using Bogus.Extensions.Brazil;
using JacksonVeroneze.TemplateWebApi.Domain.ValueObjects;

namespace JacksonVeroneze.TemplateWebApi.Util.Tests.Builders.Domain.ValueObjects;

public static class CpfValueObjectBuilder
{
    public static CpfValueObject BuildSingle(
        string? value = null)
    {
        return Factory(value).Generate();
    }

    private static Faker<CpfValueObject> Factory(
        string? value = null)
    {
        return new Faker<CpfValueObject>("pt_BR")
            .CustomInstantiator(conf =>
                CpfValueObject.Create(value ?? conf.Person.Cpf()).Value!);
    }
}
