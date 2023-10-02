using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Enums;
using JacksonVeroneze.TemplateWebApi.Domain.Specifications.Base;

namespace JacksonVeroneze.TemplateWebApi.Domain.Specifications.User;

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
