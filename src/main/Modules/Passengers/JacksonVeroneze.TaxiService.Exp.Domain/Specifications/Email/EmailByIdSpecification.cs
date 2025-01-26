using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using JacksonVeroneze.TaxiService.Exp.Domain.Specifications.Base;

namespace JacksonVeroneze.TaxiService.Exp.Domain.Specifications.Email;

[ExcludeFromCodeCoverage]
public class EmailByIdSpecification(Guid id) :
    BaseSpecification<EmailEntity>
{
    public override Expression<Func<EmailEntity, bool>> ToExpression()
    {
        return spec => spec.Id == id;
    }
}