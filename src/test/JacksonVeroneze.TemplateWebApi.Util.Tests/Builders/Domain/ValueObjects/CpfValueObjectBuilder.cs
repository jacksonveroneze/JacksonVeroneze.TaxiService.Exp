using Bogus;
using Bogus.Extensions.Brazil;
using JacksonVeroneze.TemplateWebApi.Domain.ValueObjects;

namespace JacksonVeroneze.TemplateWebApi.Util.Tests.Builders.Domain.ValueObjects;

public static class CpfValueObjectBuilder
{
    public static CpfValueObject BuildSingle()
    {
        return Factory().Generate();
    }

    private static Faker<CpfValueObject> Factory()
    {
        return new Faker<CpfValueObject>("pt_BR")
            .CustomInstantiator(conf =>
                CpfValueObject.Create(conf.Person.Cpf()).Value!);
    }
}
