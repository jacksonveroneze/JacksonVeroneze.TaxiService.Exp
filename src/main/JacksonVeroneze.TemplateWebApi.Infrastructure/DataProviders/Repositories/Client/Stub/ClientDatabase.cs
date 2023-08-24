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
                    .Select(item =>
                    {
                        ClientEntity entity = new ClientEntity(
                            new PersonName($"Client_{item}"),
                            new Email($"mail_{item}@mail.com"));

                        entity.AddAccount(new AccountEntity("1500@111", new BankEntity($"Bank_{item}")));

                        return entity;
                    }));
            }

            return Db;
        }
    }
}
