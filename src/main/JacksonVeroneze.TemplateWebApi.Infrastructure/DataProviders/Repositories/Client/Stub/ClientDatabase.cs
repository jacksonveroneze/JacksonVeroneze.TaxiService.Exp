using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.ValueObjects;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories.Client.Stub;

public static class ClientDatabase
{
    private static readonly List<ClientEntity> Db = new();

    public static IList<ClientEntity> Data
    {
        get
        {
            if (!Db.Any())
            {
                Db.AddRange(Enumerable.Range(1, 25)
                    .Select(item => new ClientEntity(
                        new PersonName($"Bank_{item}"),
                        new Email($"mail_{item}@mail.com"))));
            }

            return Db;
        }
    }
}
