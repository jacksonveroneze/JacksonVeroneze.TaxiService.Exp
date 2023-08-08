using JacksonVeroneze.TemplateWebApi.Domain.Entities;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories.Stubs.Bank;

public static class BankDatabase
{
    private static readonly List<BankEntity> _data = new();

    public static IList<BankEntity> Data
    {
        get
        {
            if (!_data.Any())
            {
                _data.AddRange(Enumerable.Range(1, 25)
                    .Select(item => new BankEntity($"Bank_{item}")));
            }

            return _data;
        }
    }
}
