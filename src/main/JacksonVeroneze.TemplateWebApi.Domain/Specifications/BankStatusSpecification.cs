using System.Linq.Expressions;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Enums;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;
using JacksonVeroneze.TemplateWebApi.Domain.Specifications.Base;

namespace JacksonVeroneze.TemplateWebApi.Domain.Specifications;

public class BankStatusSpecification : BaseSpecification<BankEntity>
{
    private readonly BankStatus _status;

    public BankStatusSpecification(BankStatus status)
    {
        _status = status;
    }

    public override Expression<Func<BankEntity, bool>> ToExpression()
    {
        return spec => spec.Status == _status;
    }

    protected override Func<BankEntity, bool> ToFunc()
    {
        return spec => spec.Status == _status;
    }
}
