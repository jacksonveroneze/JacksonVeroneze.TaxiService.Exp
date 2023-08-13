using System.Linq.Expressions;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;
using JacksonVeroneze.TemplateWebApi.Domain.Specifications.Base;

namespace JacksonVeroneze.TemplateWebApi.Domain.Specifications;

public class BankStatusSpecification : BaseSpecification<BankEntity>
{
    private readonly BankPagedFilter _filter;

    public BankStatusSpecification(BankPagedFilter filter)
    {
        _filter = filter;
    }

    protected override Expression<Func<BankEntity, bool>> ToExpression()
    {
        return spec => spec.Status ==  _filter.Status;
    }

    protected override Func<BankEntity, bool> ToFunc()
    {
        return spec => spec.Status ==  _filter.Status;
    }
}
