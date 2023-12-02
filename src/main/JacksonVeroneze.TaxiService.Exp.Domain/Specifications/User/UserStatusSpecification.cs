using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using JacksonVeroneze.TaxiService.Exp.Domain.Enums;
using JacksonVeroneze.TaxiService.Exp.Domain.Specifications.Base;

namespace JacksonVeroneze.TaxiService.Exp.Domain.Specifications.User;

[ExcludeFromCodeCoverage]
public class UserStatusSpecification : BaseSpecification<UserEntity>
{
    private readonly Expression<Func<UserEntity, bool>> _defaultExpression = _ => true;

    private readonly UserStatus? _status;

    public UserStatusSpecification(UserStatus? status)
    {
        _status = status;
    }

    public override Expression<Func<UserEntity, bool>> ToExpression()
    {
        if (!_status.HasValue)
        {
            return _defaultExpression;
        }

        return spec => spec.Status == _status;
    }
}
