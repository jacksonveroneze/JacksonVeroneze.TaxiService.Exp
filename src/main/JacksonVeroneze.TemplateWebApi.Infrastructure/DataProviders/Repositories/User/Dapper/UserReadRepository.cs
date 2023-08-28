using System.Data;
using Dapper;
using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.User;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Enums;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;
using JacksonVeroneze.TemplateWebApi.Domain.Models;
using JacksonVeroneze.TemplateWebApi.Domain.ValueObjects;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories.User.Dapper;

public class UserReadRepository : IUserReadRepository
{
    private readonly IDbConnection _repository;

    public UserReadRepository(
        IDbConnection connection)
    {
        _repository = connection;
    }

    public async Task<bool> ExistsByNameAsync(string name,
        CancellationToken cancellationToken = default)
    {
        int result = await _repository
            .RecordCountAsync<UserModel>(new { name = name });

        return result > 0;
    }

    public async Task<UserEntity?> GetByIdAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        UserModel? result = await _repository
            .GetAsync<UserModel>(id);

        if (result is null)
        {
            return null;
        }

        return new UserEntity(NameValueObject.Create(result.Name!).Value!, DateTime.Now, Gender.Male);
    }

    public Task<Page<UserEntity>> GetPagedAsync(
        UserPagedFilter filter,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
