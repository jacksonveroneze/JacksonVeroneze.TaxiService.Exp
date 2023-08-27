using System.Linq.Expressions;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Enums;
using JacksonVeroneze.TemplateWebApi.Domain.Specifications.Base;

namespace JacksonVeroneze.TemplateWebApi.Domain.Specifications.User;

public class UserStatusSpecification : BaseSpecification<UserEntity>
{
    private readonly UserStatus? _status;

    public UserStatusSpecification(UserStatus? status)
    {
        _status = status;
    }

    public override Expression<Func<UserEntity, bool>> ToExpression()
    {
        if (!_status.HasValue)
        {
            return _ => true;
        }

        return spec => spec.Status == _status;
    }
}
