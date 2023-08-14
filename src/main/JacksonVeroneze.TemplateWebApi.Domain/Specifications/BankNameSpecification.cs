using System.Linq.Expressions;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Specifications.Base;

namespace JacksonVeroneze.TemplateWebApi.Domain.Specifications;

public class BankNameSpecification : BaseSpecification<BankEntity>
{
    private readonly string? _name;

    public BankNameSpecification(string? name)
    {
        _name = name;
    }

    public override Expression<Func<BankEntity, bool>> ToExpression()
    {
        return spec => spec.Name.Contains(_name ?? string.Empty);
    }

    protected override Func<BankEntity, bool> ToFunc()
    {
        return spec => spec.Name.Contains(_name ?? string.Empty,
            StringComparison.OrdinalIgnoreCase);
    }
}
