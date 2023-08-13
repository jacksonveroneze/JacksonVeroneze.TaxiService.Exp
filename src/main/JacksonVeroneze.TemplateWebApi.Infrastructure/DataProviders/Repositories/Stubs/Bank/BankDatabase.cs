using JacksonVeroneze.TemplateWebApi.Domain.Entities;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories.Stubs.Bank;

public static class BankDatabase
{
    private static readonly List<BankEntity> Db = new();

    public static IList<BankEntity> Data
    {
        get
        {
            if (!Db.Any())
            {
                Db.AddRange(Enumerable.Range(1, 25)
                    .Select(item => new BankEntity($"Bank_{item}")));
            }

            return Db;
        }
    }
}
