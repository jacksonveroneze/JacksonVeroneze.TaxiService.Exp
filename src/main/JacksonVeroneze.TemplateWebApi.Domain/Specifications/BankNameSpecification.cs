using System.Linq.Expressions;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Specifications.Base;

namespace JacksonVeroneze.TemplateWebApi.Domain.Specifications;

public class BankNameSpecification : BaseSpecification<BankEntity>
{
    private readonly bool _matchExactly;

    private readonly string? _name;

    public BankNameSpecification(string? name,
        bool matchExactly = false)
    {
        _name = name;
        _matchExactly = matchExactly;
    }

    public override Expression<Func<BankEntity, bool>> ToExpression()
    {
        if (string.IsNullOrEmpty(_name))
        {
            return spec => true;
        }

        return spec => _matchExactly
            ? spec.Name.Equals(_name, StringComparison.OrdinalIgnoreCase)
            : spec.Name.Contains(_name);
    }

    protected override Func<BankEntity, bool> ToFunc()
    {
        return spec => string.IsNullOrEmpty(_name) ||
                       spec.Name.Contains(_name, StringComparison.OrdinalIgnoreCase);
    }
}
